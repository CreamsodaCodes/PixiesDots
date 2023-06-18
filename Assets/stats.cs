using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    
    public int creatercountCopy;
    
    void Start()
    {
       InvokeRepeating("UpdateSettings", 1f, 1f); 
    }

    void UpdateSettings()
    {
        creatercountCopy = field.CreatureCount;
    }
}
