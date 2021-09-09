using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class f1G : MonoBehaviour
{
    private Player playM;
    public float w;
    public float h;

    public ObjBase mOb;

    public List<ObjBase> mOs = new List<ObjBase>();
    public static string gType = "1F";


    void Start()
    {
        mOb = new ObjBase();
        Gv.gThis.mOm.Add(this.mOb);

        mOb.mMb = this;
        mOb.mType = f1G.gType;
        w = this.transform.localScale.x;
        h = transform.position.y + (transform.localScale.y * 2);

        playM = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
