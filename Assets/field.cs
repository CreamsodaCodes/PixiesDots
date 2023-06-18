using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class field : MonoBehaviour
{
    public static int Size = 1500;

    private static System.Random random = new System.Random();

    public static EntityClass[,] gameboard = new EntityClass[Size,Size];
    
    public static int plantCount = 0;
    public static int plantWishCount = 5000;
    public static int CreatureCount = 0;
    public static int CreatureWishCount = 2000;

    public static List<CreatureClass> allCreatures  = new List<CreatureClass>();
    


    
    void keepCreaturesAtWish(){
        if (CreatureCount<CreatureWishCount)
        {
            spawnCreature();
        }
    }
    public static void spawnCreature(){
        if (CreatureCount>CreatureWishCount)
        {
            return;
        }
        int rX = random.Next(Size);
        int rY = random.Next(Size);
        if (checkField(rX,rY)==0)
        {
            CreatureCount++;
            CreatureClass a = new CreatureClass(rX,rY,true);
            gameboard[rX,rY] = a;
            allCreatures.Add(a);
            return;
        }
    }

        public static void spawnCreature(GenomeStruct[] DNA){
        if (CreatureCount>CreatureWishCount+2000)
        {
            return;
        }
        int rX = random.Next(Size);
        int rY = random.Next(Size);
        if (checkField(rX,rY)==0)
        {
            CreatureCount++;
            CreatureClass a = new CreatureClass(rX,rY,DNA);
            gameboard[rX,rY] = a;
            allCreatures.Add(a);
            return;
        }
    } 

    public static void spawnCreatureMuated(GenomeStruct[] DNA){
        if (CreatureCount>CreatureWishCount+300)
        {
            return;
        }
        GenomeStruct[] DNACopy2;
        GenomeStruct[] DNACopy = new GenomeStruct[DNA.Length];
        int k = 0;
        foreach (GenomeStruct clone in DNA)
        {

            DNACopy[k] = clone.CloneMe();
            DNACopy[k].mutateMe();

            k++;
        }
        if(random.Next(2) == 0)
        {   if (DNA.Length >= 255)
            {
                DNACopy2 = new GenomeStruct[DNA.Length];
            }
            else
            {
                DNACopy2 = new GenomeStruct[DNA.Length+1];
                DNACopy2[DNA.Length] = new GenomeStruct(true);
            }
             
            int i = 0;
            foreach (GenomeStruct Gen in DNACopy)
            {
                Gen.mutateMe();
                DNACopy2[i] = DNACopy[i].CloneMe();
                
                i++;
            }
            
        }
        else
        {
            int i;
            if (DNA.Length == 1)
            {
                DNACopy2 = new GenomeStruct[DNA.Length];
                i = 0;
            }
            else{

                DNACopy2 = new GenomeStruct[DNA.Length-1];
                i = -1;
            }
           
            
            foreach (GenomeStruct Gen in DNACopy)
            {
                Gen.mutateMe();
                if (i>=0)
                {
                    DNACopy2[i] = DNACopy[i].CloneMe();
                }
                
                
                i++;
            }
             
        }
        
        int rX = random.Next(Size);
        int rY = random.Next(Size);
        if (checkField(rX,rY)==0)
        {
            CreatureCount++;
            CreatureClass a = new CreatureClass(rX,rY,DNACopy);
            gameboard[rX,rY] = a;
            allCreatures.Add(a);
            
        } 
        
        
        
        if (checkField(rY,rX)==0)
        {
            CreatureCount++;
            CreatureClass a = new CreatureClass(rY,rX,DNACopy2);
            gameboard[rY,rX] = a;
            allCreatures.Add(a);
            
        } 
        

        
    }
    
    
    void spawnPlant(){
        
        int rX = random.Next(Size);
        int rY = random.Next(Size);
        if (checkField(rX,rY)==0)
        {
            plantCount++;
            gameboard[rX,rY] = new PlantClass(rX,rY);
        }
    }

    public static int checkField(int x, int y)
    {
        if (x<0||x>=Size||y<0||y>=Size)
        {
            return 0;
        }
        if(field.gameboard[x,y] == null){
            return 0;
        }
        return field.gameboard[x,y].tag;
    }

    void keepPlantsAtWish(){
        if (plantCount<plantWishCount)
        {
            spawnPlant();
        }
    }

    private void Update() {
        for (int i = 0; i < 60; i++)
        {
           keepPlantsAtWish();
            keepCreaturesAtWish(); 
        }
        for (int i = 0; i < 10; i++)
        {
           keepPlantsAtWish();
             
        }
        
        List<CreatureClass> allCreaturesCopy = new List<CreatureClass>(allCreatures);
        foreach (CreatureClass Creature in allCreaturesCopy)
        {
            if (Creature.isAlive)
            {
                Creature.completeThink();
            }
            
        }
        


    }

    public void safeAllCreatures(){
        List<CreatureClass> allCreaturesCopy = new List<CreatureClass>(allCreatures);
        GenomeStruct[][] allCreaturesSafed = new GenomeStruct[allCreaturesCopy.Count][];
        BinaryFormatter formatter = new BinaryFormatter();
        int count2 = 0;

        foreach (CreatureClass cre in allCreaturesCopy)
            {
                allCreaturesSafed[count2] = cre.brainGenomes;
                count2++;
            }
        using (FileStream stream = new FileStream("genArrayCopy.dat", FileMode.Create))
        {
            
                formatter.Serialize(stream, allCreaturesSafed);
        }
            
            
        
    }

    public void loadAllCreaturesFromSafe(){
        string fileName = "genArrayCopy.dat";
        GenomeStruct[][] loadedArray;
    // Check if the file exists before attempting to load it
    if (File.Exists(fileName))
    {
        // Create a BinaryFormatter instance
        BinaryFormatter formatter = new BinaryFormatter();

        // Open the file stream for reading
        using (FileStream stream = new FileStream(fileName, FileMode.Open))
        {
            
            // Deserialize the data and cast it back to the appropriate type
            loadedArray = (GenomeStruct[][])formatter.Deserialize(stream);

            // Use the loaded data as needed
            // ...
        }
        foreach (GenomeStruct[] RNA in loadedArray)
        {
            spawnCreature(RNA);
        }

    }}

    
}
