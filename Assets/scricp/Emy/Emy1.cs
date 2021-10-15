using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Emy1 : MonoBehaviour, ObjInterface
{
    public static string gType = "Enemy1";
    //  private List<ObjBase> play;
    public ObjBase mOb;
    public EmyBase emy;
    //public Vector3 getPos()
    //{
    //    return this.transform.position;
    //}
    void Start()
    {
        mOb = new ObjBase();
        emy = new EmyBase();
        emy.rangex = 0.5f;
        emy.rangey = 0.5f;
        mOb.speed = 4f;
        mOb.mMb = this;
        mOb.mType =Emy1.gType;
        mOb.ani = GetComponent<Animator>();

        mOb.HitboxR = this.transform.Find("HItBoxRight").gameObject;
        mOb.HitboxR.SetActive(false);
        mOb.HitboxL = this.transform.Find("HItBoxLeft").gameObject;

        mOb.HitboxL.SetActive(false);
        Gv.gThis.mOm.Add(this.mOb);

        
        emy.oldPos = mOb.getPos();
        transform.position = new Vector3(transform.position.x, mOb.floor1y, transform.position.z);
        mOb.plusExp = 50;

    }
    private void Update()
    {
        Om om = Gv.gThis.mOm;
        mOb.time += Time.deltaTime;
        mOb.attacktime += Time.deltaTime; 
        emy.oldPos = mOb.getPos();
        List<ObjBase> play = Gv.gThis.mOm.FindPlayer();
        emy.targertOb = (Player)play[0].mMb;
        emy.targetPos = play[0].getPos();
        emy.targetRad = play[0].getRadius();

        



        if (play.Count > 0&&mOb.die==false)
        {
            emy.Move(play, emy.targetPos, emy.targetRad, gameObject.transform, mOb);
        }
    }

    private void Die()
    {
        mOb.Die(this.gameObject);
    }
    private void Attack()
    {
        emy.Attack(mOb,this.gameObject);

    }
    public void AttackEnd()
    {
        mOb.AttackEnd(mOb);

    }
    private void HitEnd()
    {
        mOb.HitEnd(mOb);
    }

    //private void PyAttck(List<ObjBase> fos)
    //{




    //    for (int i = 0; i < fos.Count; i++)
    //    {

    //        MonoBehaviour pl = fos[i];


    //        if (pl.name == "Player")
    //        {

    //            Player pe = (Player)pl;


    //            pe.mOb.curhp -= this.mOb.attackp;
    //            Debug.Log(pl.name + " Hit ");

    //            if (pe.mOb.curhp <= 0)
    //            {
    //                Debug.Log("Dead");

    //                Destroy(pe.gameObject);
    //                Gv.gThis.mOm.Remove(pe);
    //            }





    //        }


    //    }


    //}
    //public void OnAttack(MonoBehaviour targetmb)
    //{
    //    //throw new System.NotImplementedException();
    //    if (targetmb.name.Equals("Player"))
    //    {

    //        Player pl = (Player)targetmb;
    //        pl.mOb.curhp -= mOb.attackp;
    //        Debug.Log(targetmb.name + " Hit " + mOb.attackp);

    //        if (pl.mOb.curhp <= 0)
    //        {
    //            Debug.Log("Dead");

    //            Destroy(pl.gameObject);
    //            Gv.gThis.mOm.Remove(pl.mOb);

    //        }

    //    }
    //    else
    //    {
    //        Debug.Log("실패");
    //    }
    //}


}


