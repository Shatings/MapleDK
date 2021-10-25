using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summons : MonoBehaviour
{
    public float deatime;
    public ObjBase mOb;

    // Start is called before the first frame update
    void Start()
    {
        mOb = new ObjBase();    
        Invoke("DestroyOb", deatime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
