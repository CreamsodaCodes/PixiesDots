
using UnityEngine;
public class HormoneClass{
    int decayTimer;
    int HormoneType;
    int xPos;
     int yPos;
    GameObject spawnedPrefab;
    public void spawnVisualHormone(int xCord,int yCord){
        GameObject prefab = Resources.Load<GameObject>("HormoneSprite");
        Vector3 spawnPoint = new Vector3(xCord, yCord, 1);
        spawnedPrefab = GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
        
    }

    public bool runAndKillHormone(){
        decayTimer--;
        if (decayTimer<=0)
        {
            GameObject.Destroy(spawnedPrefab);
            return true;
        }
        return false;
    }
}