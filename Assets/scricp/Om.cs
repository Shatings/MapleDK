using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Om //<_T1> //ObjMgr
{
    public List<ObjBase> mOs = new List<ObjBase>();
    public GameObject tmonster;
    public Emy1 ems;
    public int j = 1;
    public Dictionary<int, ObjBase> valuePairs = new Dictionary<int, ObjBase>
    {

    };


    public void Add(ObjBase obj)
    {
        mOs.Add(obj);
        valuePairs.Add(j, obj);
        Debug.LogWarning(" " + valuePairs);
        j++;
    }
    //Sub/Remove
    public void Remove(ObjBase obj)
    {
        mOs.Remove(obj);
        j--;
        valuePairs.Remove(j);

        Debug.LogWarning(" " + valuePairs);
    }
    public void Clear()
    {
        j = 1;
        valuePairs.Clear();
        mOs.Clear();
    }

    public List<ObjBase> findPos(float tx, float ty,float w,float h)
    {
        List<ObjBase> rst = new List<ObjBase>();


        for (int i = 0; i < mOs.Count; i++) {


            if (mOs[i].getPos().x - tx <= w / 2 && mOs[i].getPos().x - tx >= -w / 2 &&
                mOs[i].getPos().y - ty <= h / 2 && mOs[i].getPos().y - ty >= -h / 2)
            {




                rst.Add(mOs[i]);

            }
        }
        // ...
        //rst.Add(mOs[0]);
    
        return rst;
    }
    public List<ObjBase> find2fG(float x, float y, float oldy)
    {
        List<ObjBase> rst = new List<ObjBase>();
        for (int i = 0; i < mOs.Count; i++)
        {
            if (mOs[i].mType != F2Ground.gType)
            { continue; }
            if (mOs[i].getPos().y>y&&mOs[i].getPos().y<oldy&&
                mOs[i].getPos().x + mOs[i].getTest().x/2>x&&mOs[i].getPos().x-mOs[i].getTest().x / 2<x )
            {
                Debug.Log(" " + mOs[i].mType);
                rst.Add(mOs[i]);
            }

        }

         
        return rst;
    }
    public List<ObjBase> find1fG(float x, float y, float oldy)
    {
        List<ObjBase> rst = new List<ObjBase>();
        for (int i = 0; i < mOs.Count; i++)
        {
            if (mOs[i].mType !=f1G.gType)
            { continue; }
            if (mOs[i].getPos().y > y && mOs[i].getPos().y < oldy &&
                mOs[i].getPos().x + mOs[i].getTest().x / 2 > x && mOs[i].getPos().x - mOs[i].getTest().x / 2 < x)
            {
                Debug.LogError(" " + mOs[i].mType);
                rst.Add(mOs[i]);
            }

        }


        return rst;
    }






    public List<ObjBase> FindPlayer()
    {
        List<ObjBase> rst = new List<ObjBase>();
     

        for (int i = 0; i < mOs.Count; i++)
        {
           
            if (mOs[i].mType.Equals(Player.gType))
            {
                rst.Add(mOs[i]);
            }
           


            
        }
        // ...
        //rst.Add(mOs[0]); 

        return rst;
    }

    
}
