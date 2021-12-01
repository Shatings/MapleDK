using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emy3 : MonoBehaviour, ObjInterface
{
    public static string gType = "Enemy3";
    public ObjBase mOb = new ObjBase();
    public EmyBase emy;

    //public Vector3 getPos()
    //{
    //    return this.transform.position;
    //}
    void Start()
    {
        mOb = new ObjBase();
        emy = new EmyBase();
        mOb.speed  = 7;
        emy.rangex = 0.5f;
        emy.rangey = 0.5f;
        mOb.mMb = this;
        mOb.mType = Emy3.gType;
        mOb.ani = GetComponent<Animator>();
        mOb.maxhp = 1;
        mOb.curhp = 1;
        mOb.attackp = 1;

        mOb.Hitbox = this.transform.Find("HItBoxRight").gameObject;
        mOb.Hitbox.SetActive(false);
        //mOb.HitboxL = this.transform.Find("HItBoxLeft").gameObject;

        //mOb.HitboxL.SetActive(false);
        Gv.gThis.mOm.Add(this.mOb);


        emy.oldPos = mOb.getPos();
        transform.position = new Vector3(transform.position.x, mOb.floor1y, transform.position.z);
        mOb.plusExp = 300;
        mOb.point = 1;
        mOb.transform = this.transform;

    }
    private void Update()
    {
        Om om = Gv.gThis.mOm;
        mOb.time += Time.deltaTime;
        mOb.attacktime += Time.deltaTime;
        emy.oldPos = mOb.getPos();
        try
        {
            List<ObjBase> play = Gv.gThis.mOm.FindPlayer();
            emy.targertOb = (Player)play[0].mMb;
            emy.targetPos = play[0].getPos();
            emy.targetRad = play[0].getRadius();
            if (play.Count > 0 && mOb.die == false)
            {
                emy.Move(play, emy.targetPos, emy.targetRad, gameObject.transform, mOb);
            }
        }
        catch (Exception e)
        {
            Debug.Log("오류" + e);
        }
    }

    private void FixedUpdate()
    {
        if (emy.attack)
        {
            emy.APS(mOb,transform);
        }
    }

    private void Die()
    {
        mOb.Die(this.gameObject);
    }
    private void Attack()
    {
        emy.Attack(mOb, this.gameObject);
        emy.attack = true;

    }
    public void AttackEnd()
    {
        mOb.AttackEnd(mOb);

    }
    private void HitEnd()
    {
        mOb.HitEnd(mOb);
    }
}
