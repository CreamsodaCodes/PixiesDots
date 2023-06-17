 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantClass  : EntityClass
{
 
    
    public PlantClass(int _xCord,int _yCord): base( _xCord, _yCord){
        
        tag = 2;
        visualName = "PlantVisual";
        spawnVisual(xCord,yCord);
    }
}
