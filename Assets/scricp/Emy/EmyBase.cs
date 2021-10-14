using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmyBase //: //ObjBase
{
    
    public float rangex;
    public float rangey;
    public float lowspeed=0.5f;
   
    public Player targertOb;
    public Vector3 targetPos;
    public Vector3 targetRad;
    public Vector3 oldPos;
    public float f1jumping = 0f;
    private void AttackCheck(ObjBase mOb,Transform oldpos)
    {
        GameObject hitbox;
        hitbox = mOb.GetHitBox();
        List<ObjBase> fos = mOb.Httest(hitbox);
        mOb.ani.SetBool("Right", (targetPos.x > oldpos.position.x) ? true : false);
        for (int i = 0; i < fos.Count; i++)
        {
            if (fos[i].mType == Player.gType)
            {
                mOb.ani.SetBool("Attack", true);
            }
        }
    }
    public void Move(List<ObjBase> play, Vector3 targetPos, Vector3 targetRad,Transform oldpos,ObjBase mOb)
    {
        AttackCheck(mOb, oldpos);
        if (Mathf.Abs(targetPos.x - oldPos.x) > targetRad.x&&!mOb.ani.GetBool("Attack"))
        {

            mOb.righ = (targetPos.x > oldpos.position.x) ? "Right" : "Left";
            oldpos.position = new Vector3(oldpos.position.x + ((targetPos.x > oldpos.position.x) ? +Time.deltaTime : -Time.deltaTime) * mOb.speed, oldpos.position.y, 0);

            mOb.ani.SetBool("Move", true);
        }
        else
        {

            mOb.ani.SetBool("Move", false);

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
                Debug.Log("타입+"+ mOb.mType);
                if (fos[i].mType == Player.gType)
                {
                    if (mOb.mType == Emy3.gType)
                    {
                        Debug.Log("해치웠나");
                        fos[i].ani.SetFloat("Debuff", lowspeed);
                        fos[i].speed = fos[i].basicspeed/2;
                    }
                    hitbox.SetActive(true);

                    mOb.Attack1(targertOb.mOb,gameObject);
                    
                    hitbox.SetActive(false);

                }

            }
        }
    }
   
}
