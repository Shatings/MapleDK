using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int id;
    public int startpos;
    public float successRate;
    public float destroyRate;



    public Item(string Name, int Id,int pos,float suces)
    {
        itemName = Name;

        id = Id;
        startpos = pos;
        successRate = suces;
        
    }


    // Start is called before the first frame update

}
