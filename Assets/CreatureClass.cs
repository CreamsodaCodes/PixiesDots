using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class CreatureClass: EntityClass
{
    
    public NodeClass[] allInternalNodes;
    public GenomeStruct[] brainGenomes;
     List<NodeClass> connectorNodes; //remove public after test!
    int maxRepititionsPerThink = 10;
     public float[] globalInputs;
     public float[] globalOutputs;
    //converted
    int food = 4000;
    
    // new not yet mutation variables:
    int visionLength = 10;



    public CreatureClass(int _xCord,int _yCord,GenomeStruct[] _brainGenomes): base( _xCord, _yCord){
        tag = 1;
        globalInputs = new float[256];
        
        globalOutputs = new float[256];
        brainGenomes = new GenomeStruct[_brainGenomes.Length];
        int count = 0;

        foreach (GenomeStruct Gen in _brainGenomes)
        {
            brainGenomes[count]= Gen.CloneMe();
            count++;
        }
        connectorNodes = new List<NodeClass>();
        brainBuilder(brainGenomes);
        spawnVisual(xCord,yCord,brainGenomes[0].red,brainGenomes[0].green,brainGenomes[0].blue);
        moveRight();
    }
    public CreatureClass(int _xCord,int _yCord,bool random): base( _xCord, _yCord){
        tag = 1;
        globalInputs = new float[256];
        
        globalOutputs = new float[256];
        GenomeStruct[] _brainGenomes = new GenomeStruct[5];
        int i = 0;
        foreach (GenomeStruct item in _brainGenomes)
        {
            _brainGenomes[i] = new GenomeStruct(true);
            i++;
        }
        connectorNodes = new List<NodeClass>();
        brainGenomes = _brainGenomes;
        brainBuilder(_brainGenomes);
        spawnVisual(xCord,yCord,brainGenomes[0].red,brainGenomes[0].green,brainGenomes[0].blue);

    }


    



    void brainBuilder(GenomeStruct[] brainGenomes){
        allInternalNodes = new NodeClass[256];
        int run = 1;
        List<byte> validTargetIDs = new List<byte>();
        List<GenomeStruct> deleteThose = new List<GenomeStruct>();
        
        List<GenomeStruct> copyBrainGenomes = brainGenomes.ToList();
       
        foreach (GenomeStruct Genome in copyBrainGenomes)
        {
            if (!Genome.sink && Genome.source ) //Letzte Node aber nicht erste 
            {
                if (allInternalNodes[Genome.sourceId] == null)
                {
                    allInternalNodes[Genome.sourceId] = new NodeClass(false,false,allInternalNodes,globalInputs,globalOutputs,Genome.bias);
                    allInternalNodes[Genome.sourceId].addConnectionEnd(Genome.targetId,Genome.weight);
                    validTargetIDs.Add(Genome.sourceId);
                }
                else
                {
                    allInternalNodes[Genome.sourceId].addConnectionEnd(Genome.targetId,Genome.weight);
                }
                
            } 
            //Erste und Letzte =
            else if(!Genome.sink && !Genome.source){
                connectorNodes.Add(new NodeClass(true,globalInputs,globalOutputs,Genome.weight,Genome.sourceId,Genome.targetId));
            }

        }
        while (run!=0)
        {
            
           run = 0;
            List<byte> validTargetIDs2 = new List<byte>();
            foreach (GenomeStruct Genome in copyBrainGenomes)
            {
                if (Genome.sink && validTargetIDs.Contains(Genome.targetId))
                {
                    if (Genome.source)
                    {
                        if (allInternalNodes[Genome.sourceId]==null)
                        {
                            allInternalNodes[Genome.sourceId]= new NodeClass(false,false,allInternalNodes,globalInputs,globalOutputs,Genome.bias);
                            allInternalNodes[Genome.sourceId].addConnectionMiddel(Genome.targetId,Genome.weight);
                            validTargetIDs2.Add(Genome.sourceId);
                            deleteThose.Add(Genome);
                            run++;
                        }
                        else
                        {
                           allInternalNodes[Genome.sourceId].addConnectionMiddel(Genome.targetId,Genome.weight);
                           deleteThose.Add(Genome);
                        }
                    }
                    else
                    {
                         allInternalNodes[Genome.targetId].addConnectionStart(Genome.sourceId,Genome.weight);
                         allInternalNodes[Genome.targetId].isInput = true;
                         deleteThose.Add(Genome);
                         run++;
                    }
                }
            }

            
            validTargetIDs = validTargetIDs2;
            validTargetIDs2 = null;
            copyBrainGenomes.RemoveAll(item => deleteThose.Contains(item));
        }
        //return allInternalNodes;
    }


    public void think(){
        resetoutputs();
        int processCount =0;
        
        bool run = true;
        foreach (NodeClass Node in connectorNodes) 
        {
            Node.push(); //to be tested
        }
        foreach (NodeClass Node in allInternalNodes) 
        {
            if (Node!=null&&Node.isInput) 
            {
                Node.getInput();
                Node.push();
            }

        }
        while (run)
        {
            run = false;
            foreach (NodeClass Node in allInternalNodes)
            {
                if(Node!=null&&Node.input!=0){
                    Node.push();
                    run = true;
                }
            }
            processCount++;
            if (processCount==maxRepititionsPerThink)
            {
                run=false;
            }
        }
        

    }

    public void completeThink(){
        inputManager();
        think();
        moveDecicion();
        food -= brainGenomes.Count();
        
        if (food<=0)
        {
            killMe();
        }

    }

    public override void killMe(){
        isAlive = false;
        field.gameboard[xCord,yCord] = null;
        field.allCreatures.Remove(this);
        field.CreatureCount--;
        deleteVisual();
        
    }

    void resetoutputs(){
        for (int i = 0; i < globalOutputs.Length; i++)
        {
            globalOutputs[i] = 0;
        }
    }
    void resetinputs(){
        for (int i = 0; i < globalInputs.Length; i++)
                {
                    globalInputs[i] = 0;
                }    
    }

    void eat(int xCord,int yCord){
        if (field.gameboard[xCord,yCord] == null)
        {
            return;
        }
        field.gameboard[xCord,yCord].deleteVisual();
        food += 900;
        field.plantCount--;
        field.spawnCreatureMuated(brainGenomes);
    }

    void eatCreature(int xCord,int yCord){
        if (field.gameboard[xCord,yCord] == null)
        {
            return;
        }
        field.gameboard[xCord,yCord].deleteVisual();
        field.gameboard[xCord,yCord].killMe();
        food += 500;

        
    }
    void moveLeft(){
        if (xCord-1<0)
        {
            return;
        }
        int check = checkField(xCord-1,yCord);
        if (check == 1)
        {
            eatCreature(xCord-1,yCord);
        }
        if (check == 2)
        {
            eat(xCord-1,yCord);
        }
        field.gameboard[xCord,yCord] = null;
        xCord = xCord-1;
        field.gameboard[xCord,yCord] = this;
        updatePosition(xCord,yCord);
    }

        void moveRight(){
        if (xCord+1>=field.Size)
        {
            return;
        }
        int check = checkField(xCord+1,yCord);
        if (check == 1)
        {
            eatCreature(xCord+1,yCord);
        }
        if (check == 2)
        {
            eat(xCord+1,yCord);
        }
        field.gameboard[xCord,yCord] = null;
        xCord = xCord+1;
        field.gameboard[XCord,YCord] = this;
        updatePosition(xCord,yCord);
    }

        void moveUp(){
        if (yCord+1>=field.Size)
        {
            return;
        }
        int check = checkField(xCord,yCord+1);
        if (check == 1)
        {
            eatCreature(xCord,yCord+1);
        }
        if (check == 2)
        {
            eat(xCord,yCord+1);
        }
        field.gameboard[XCord,YCord] = null;
        yCord = yCord+1;
        field.gameboard[XCord,YCord] = this;
        updatePosition(xCord,yCord);
    }
     void moveDown(){
        if (yCord-1<0)
        {
            return;
        }
        int check = checkField(xCord,yCord-1);
        if (check == 1)
        {
            eatCreature(xCord,yCord-1);
        }
        if (check == 2)
        {
            eat(xCord,yCord-1);
        }
        field.gameboard[XCord,YCord] = null;
        yCord = yCord-1;
        field.gameboard[XCord,YCord] = this;
        updatePosition(xCord,yCord);
    }
    void giveBirth(){
        if(food<3000){
            return;
        }
        food -= 3000;
        
        field.spawnCreatureMuated(brainGenomes);

    }

    void moveDecicion(){
        switch (GetMaxIndexFirst5())
    {
        case 1:
            moveDown();
            break;
        case 2:
            moveUp();
            break;
        case 3:
            moveLeft();
            break;
        case 4:
            moveRight();
            break;
        case 5:
            giveBirth();
            break;
        
        // Add more cases as needed for additional output neurons
        default:
            break;
    }
    }
bool clock1hz = true;
    void inputManager(){
        resetinputs();
        globalInputs[0] = VisualCortex.GetLineFields(xCord,yCord,visionLength,1,1); //creatures north
        globalInputs[1] = VisualCortex.GetLineFields(xCord,yCord,visionLength,1,2); //creatures easr
        globalInputs[2] = VisualCortex.GetLineFields(xCord,yCord,visionLength,1,3); //creatures south
        globalInputs[3] = VisualCortex.GetLineFields(xCord,yCord,visionLength,1,4); //creatures west
        globalInputs[4] = VisualCortex.GetLineFields(xCord,yCord,visionLength,2,1); //plants north
        globalInputs[5] = VisualCortex.GetLineFields(xCord,yCord,visionLength,2,2); //plants easr
        globalInputs[6] = VisualCortex.GetLineFields(xCord,yCord,visionLength,2,3); //plants south
        globalInputs[7] = VisualCortex.GetLineFields(xCord,yCord,visionLength,2,4); //plants west
        globalInputs[8] = 1f;
        globalInputs[9] = 10f;
        globalInputs[10] = -1f;
        globalInputs[11] = -11f;
        if (clock1hz)
        {
            clock1hz = false;
            globalInputs[12] = 11f;
        }
        else{
            clock1hz = true;
            globalInputs[12] = -11f;
        }
        globalInputs[13] = VisualCortex.GetLineFields(xCord,yCord,visionLength*4,1,1); //creatures north
        globalInputs[14] = VisualCortex.GetLineFields(xCord,yCord,visionLength*4,1,2); //creatures easr
        globalInputs[15] = VisualCortex.GetLineFields(xCord,yCord,visionLength*4,1,3); //creatures south
        globalInputs[16] = VisualCortex.GetLineFields(xCord,yCord,visionLength*4,1,4); //creatures west
        globalInputs[17] = VisualCortex.GetLineFields(xCord,yCord,visionLength*4,2,1); //plants north
        globalInputs[18] = VisualCortex.GetLineFields(xCord,yCord,visionLength*4,2,2); //plants easr
        globalInputs[19] = VisualCortex.GetLineFields(xCord,yCord,visionLength*4,2,3); //plants south
        globalInputs[20] = VisualCortex.GetLineFields(xCord,yCord,visionLength*4,2,4);
        globalInputs[21] = Nose.smell(xCord,yCord,4,1);
        globalInputs[22] = Nose.smell(xCord,yCord,4,2);
    }

    
public int GetMaxIndexFirst5()
{
    int maxIndex = 0;
    float maxValue = globalOutputs[0];

    for (int i = 1; i < 5; i++)
    {
        if (globalOutputs[i] > maxValue)
        {
            maxValue = globalOutputs[i];
            maxIndex = i;
        }
    }

    return maxIndex;
}



    




    
}