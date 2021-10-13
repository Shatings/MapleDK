using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmyBase //: //ObjBase
{
    public float movespeed = 0.9f;
    public float rangex;
    public float rangey;
   
    public Player targertOb;
    public Vector3 targetPos;
    public Vector3 targetRad;
    public Vector3 oldPos;
    public float f1jumping = 0f;

    public void Move(List<ObjBase> play, Vector3 targetPos, Vector3 targetRad,Transform oldpos,ObjBase mOb)
    {

        if (Mathf.Abs(targetPos.x - oldPos.x) > targetRad.x)
        {

            mOb.righ = (targetPos.x > oldpos.position.x) ? "Right" : "Left";
            oldpos.position = new Vector3(oldpos.position.x + ((targetPos.x > oldpos.position.x) ? +Time.deltaTime : -Time.deltaTime) * movespeed, oldpos.position.y, 0);

            mOb.ani.SetBool("Move", true);
        }
        else
        {

            mOb.ani.SetBool("Right", (mOb.righ == "Right") ? true : false);
            mOb.ani.SetBool("Attack", true);

        }
    }
    public void Attack(ObjBase mOb,GameObject gameObject)
    {
        Debug.Log("공격");
        GameObject hitbox = mOb.GetHitBox();
        List<ObjBase> fos = mOb.Httest(hitbox);
        Debug.Log(hitbox.name);
        Debug.Log("숫자" + fos.Count);
        if (targertOb.inv == false)
        {
            for (int i = 0; i < fos.Count; i++)
            {

                if (fos[i].mType == Player.gType)
                {
                    hitbox.SetActive(true);

                    mOb.Attack1(targertOb.mOb,gameObject);
                    
                    hitbox.SetActive(false);

                }

            }
        }
    }
   
}
