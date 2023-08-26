using System;
[Serializable]
public struct GenomeStruct
{
    // Struct fields or properties
    public bool source; //false means input 
    public bool sink;
    public byte sourceId;
    public byte targetId;
    public float weight;
    private static Random random = new Random();

    public float bias;

    public float red;
    public float green;
    public float blue;
    int inputAmount; //40; //256
    int outputAmount;
    public int WhatFunction;
    public float size;
    // Struct constructor
    public GenomeStruct(bool source, bool sink, byte sourceId, byte targetId, float weight, float bias,float r,float g,float b,int whatFunctio,float size)
    {
        this.inputAmount = 99;
        this.outputAmount = 99;
        this.source = source;
        this.sink = sink;
        this.sourceId = sourceId;
        this.targetId = targetId;
        this.weight = weight;
        this.bias = bias;
        this.red = r;
        this.blue = b;
        this.green = g;
        this.WhatFunction = whatFunctio;
        this.size = size;
    }

    public GenomeStruct(bool genRandom)
    {
        this.inputAmount = 99;
         this.outputAmount = 99;
        source = random.Next(2) == 0;
        sink = random.Next(2) == 0;
        sourceId = (byte)random.Next(outputAmount);
        targetId = (byte)random.Next(outputAmount);
        weight = (float)(random.NextDouble() * 7.0 - 3.5);
        bias  = (float)((random.NextDouble() * 0.02) - 0.01);
        red = (float)random.NextDouble();
        green = (float)random.NextDouble();
        blue = (float)random.NextDouble();
        WhatFunction = random.Next(6);
        size = (float)random.NextDouble()*0.08f+0.1f;
    }
    public GenomeStruct CloneMe(){
        return new GenomeStruct(source,sink,sourceId,targetId,weight,bias,red,green,blue,WhatFunction,size);
    }

    

    public void mutateMe(){
        int mutator = random.Next(101);

        int randomInt = random.Next(2) * 2 - 1;
        float randomGaussian = (float) (random.NextDouble() * 7.0 - 3.5);;
        if (mutator==1)
        {
            this.source = !source;
            

        }
        if (mutator==2)
        {
            
            this.sink = !sink;

        }
        if (2<mutator&&mutator<6)
        {
            if(this.sourceId + randomInt>0&&this.sourceId + randomInt<outputAmount+1){
                this.sourceId += (byte)randomInt;
            }
            
            
        }
        if (6<mutator&&mutator<11)
        {
            if(this.targetId + randomInt>0&&this.targetId + randomInt<outputAmount+1){
                this.targetId += (byte)randomInt;
            }
            
            
        }
        if (11<mutator&&mutator<21)
        {
             
        
            this.weight += randomGaussian;
        
        }
        if (21<mutator&&mutator<31)
        {
            this.bias += (float)((random.NextDouble() * 0.02) - 0.01);
        }
        if (31<mutator&&mutator<36)
        {
            float change = (float)((random.NextDouble() * 0.2) - 0.1);
            if (this.red+change>0&&this.red+change<1)
            {
                this.red += change;
            }
            
        }
        if (41<mutator&&mutator<50)
        {
            float change = (float)((random.NextDouble() * 0.2) - 0.1);
            if (this.blue+change>0&&this.blue+change<1)
            {
                
                this.blue += change;
                
            }
        }
        if (51<mutator&&mutator<60)
        {
            float change = (float)((random.NextDouble() * 0.2) - 0.1);
            if (this.green+change>0&&this.green+change<1)
            {
                this.green += change;
            }
        }
        if (61<mutator&&mutator<65)
        {
           int rnumber = random.Next(3)-1;
           if (WhatFunction + rnumber >= 0 && WhatFunction + rnumber <6)
           {
            WhatFunction += rnumber;
           }
        }
        if (65<mutator&&mutator<75)
        {
            float sizeAddition = (float)random.NextDouble()*0.5f-0.025f;
            if (sizeAddition+size<0.18&&sizeAddition+size>0.1)
            {
                size += sizeAddition;
            }
        }
    }
}