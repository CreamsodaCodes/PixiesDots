using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityClass
{
    public double redness; 
    public int food;
    public EntityClass(int _xCord,int _yCord){
        xCord = _xCord;
        yCord = _yCord;
    }
    public int tag;
    public bool enabled = true; 
    public  int xCord;
    public int yCord;
    public bool isAlive = true;
    public int sizeFood; //converted size
     public int XCord
    {
        get { return xCord; }
        set { xCord = value; }
    }

    public int YCord
    {
        get { return yCord; }
        set { yCord = value; }
    }

    
    public int checkField(int x, int y)
    {
        if(field.gameboard[x,y] == null){
            return 0;
        }
        return field.gameboard[x,y].tag;
    }

    public string visualName = "CreatureVisual";
    public GameObject spawnedPrefab;
    public void spawnVisual(int xCord,int yCord,float r,float g,float b,float size){
        GameObject prefab = Resources.Load<GameObject>(visualName);
        Vector3 spawnPoint = new Vector3(xCord, yCord, 1);
        spawnedPrefab = GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
        spawnedPrefab.GetComponent<SpriteRenderer>().color = new Color(r, g, b, 1);
        spawnedPrefab.transform.localScale = new Vector3(size, size, size);
    }
    public void spawnVisual(int xCord,int yCord){
        GameObject prefab = Resources.Load<GameObject>(visualName);
        Vector3 spawnPoint = new Vector3(xCord, yCord, 1);
        spawnedPrefab = GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
        
    }
    public void updatePosition(int xCord,int yCord){
        if (spawnedPrefab == null)
        {
            return;
        }
        Vector3 newPosition = new Vector3(xCord, yCord, 1);
        spawnedPrefab.transform.position = newPosition;
    }

    public void deleteVisual(){
        GameObject.Destroy(spawnedPrefab);
    }
    public virtual void killMe(){}

    public void grow(){
    spawnedPrefab.transform.localScale = spawnedPrefab.transform.localScale*2f;    }
    public void shrink(){
    spawnedPrefab.transform.localScale = spawnedPrefab.transform.localScale*0.5f;    }
}
