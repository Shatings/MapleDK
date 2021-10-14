using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class f2G : MonoBehaviour
{
    private Player playM;
    public float w;
    public float h;
    float distx;
    float distx2;
    float disty;
    float disty2;
    public ObjBase mOb;
    public List<ObjBase> mOs = new List<ObjBase>();
    public static string gType = "2F";


    void Start()
    {
        mOb = new ObjBase();


        mOb.mMb = this;
        mOb.mType = f2G.gType;
        w = this.transform.localScale.x;
        h = this.transform.localScale.y;

        playM = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        


    }
  
        
}

