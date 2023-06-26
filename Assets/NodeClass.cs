using System;
using System.Collections.Generic;
using UnityEngine;

public class NodeClass
{
    
    float bias;
   public float input;
    public float output;
    public bool isInput;
    public bool isConnector;
    float connectorWeight;
    int connectorSource;
    int connectorTarget;
    NodeClass[] internalNodes;
    float[] globalInputsRef;
    float[] globalOutputsRef;
    Dictionary<int, float> connectionDictonary;//target,weight
    Dictionary<int, float> startPullDictonary;//source,weight
    Dictionary<int, float> endPushDictonary;//target,weight

    int function = 0; //0 = Sigmoid 1 = 


    public NodeClass(bool _isInput, bool _isConnector, NodeClass[] _internalNodes, float[] _globalInputs, float[] _globalOutputs, float _bias,int _function)
    {
        this.isInput = _isInput;
        this.isConnector = _isConnector;
        this.internalNodes = _internalNodes;
        this.globalInputsRef = _globalInputs;
        this.globalOutputsRef = _globalOutputs;
        connectionDictonary = new Dictionary<int, float>();//target,weight
        startPullDictonary = new Dictionary<int, float>();
        endPushDictonary = new Dictionary<int, float>();
        this.bias = _bias;
        this.function = _function;
    }
        public NodeClass(bool _isConnector, float[] _globalInputs, float[] _globalOutputs, float _connectorWeight, int _connectorSource, int _connectorTarget)
    {
        //Connector 
        this.isConnector = _isConnector;
        
        this.globalInputsRef = _globalInputs;
        this.globalOutputsRef = _globalOutputs;
        this.connectorWeight = _connectorWeight;
        this.connectorSource = _connectorSource;
        this.connectorTarget = _connectorTarget;
        
    }


    public void addInput(float value)
    {
        input += value;
    }

    public void push(){
        
        if (!isConnector)
        {
            
           output = Funcion(input,function);
           foreach (KeyValuePair<int, float> connection in connectionDictonary)
            {
                
                if (connection.Key>41) //255
                {
                    Debug.Log("Connection internal Key bigger than 255");
                }
                
                internalNodes[connection.Key].addInput(output*connection.Value); 
            
            }
            foreach (KeyValuePair<int, float> connection in endPushDictonary)
            {
                //Debug
                if (connection.Key>41)//255
                {
                    Debug.Log("Connection Output Key bigger than 255");
                }
                globalOutputsRef[connection.Key] += output*connection.Value; 
            
            } 
        }
        else
        {
            if (connectorTarget>41||connectorSource>41) //255
                {
                    Debug.Log("Connection Target or Source bigger than 255");
                }
            globalOutputsRef[connectorTarget] += globalInputsRef[connectorSource] * connectorWeight;
        }
        input = 0;
        
    }

    public void getInput(){
        
        foreach (KeyValuePair<int, float> connection in startPullDictonary)
        {
            input += globalInputsRef[connection.Key]*connection.Value;
        }
    }

    public void addConnectionEnd(int target,float weight){
        if (endPushDictonary.ContainsKey(target)){
            return;
        }
        endPushDictonary.Add(target,weight);
    }
    public void addConnectionMiddel(int target,float weight){
        if (connectionDictonary.ContainsKey(target)){
            return;
        }
        connectionDictonary.Add(target,weight);
    }
    public void addConnectionStart(int source,float weight){
        if (startPullDictonary.ContainsKey(source)){
            return;
        }
        startPullDictonary.Add(source,weight);
    }

    public void changeBias(float amount){
        bias += amount;
    }

    float Funcion(float input, int function)
{
    switch (function)
    {
        case 1:
            return (float)(1.0 / (1.0 + Math.Exp(-input)));

        case 2:
            // ReLU function
            return Math.Max(0, input);

        case 3:
            // Leaky ReLU function
            const float leakyFactor = 0.01f;
            return input >= 0 ? input : leakyFactor * input;

        case 4:
            // Hyperbolic Tangent (tanh) function
            return (float)Math.Tanh(input);

        case 5:
            // Binary Step function
            return input >= 0 ? 1 : 0;

        

        // Add more cases as needed for additional functions
        default:

            return Math.Max(0, input);
    }

    // Return a default value or handle an invalid function number
    throw new ArgumentException("Invalid function number.");
}



}