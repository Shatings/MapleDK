using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Emy1 : MonoBehaviour, ObjInterface
{
    public static string gType = "Enemy1";
    //  private List<ObjBase> play;
    public ObjBase mOb;
    public EmyBase emy;
    public float f1jumping = 0f;
    public Vector3 oldPos;

    private Vector3 vector;
    private enum em
    {
        Find,
        Move,
        Idem

    }
    private em emd=em.Idem;

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
        mOb.mMb = this;
        mOb.mType =Emy1.gType;
        mOb.ani = GetComponent<Animator>();
        
        mOb.HitboxR = this.transform.Find("HItBoxRight").gameObject;
        mOb.HitboxR.SetActive(false);
        mOb.HitboxL = this.transform.Find("HItBoxLeft").gameObject;
        
        mOb.HitboxL.SetActive(false);
        Gv.gThis.mOm.Add(this.mOb);

        f1jumping = 0f;
        oldPos = mOb.getPos();
        transform.position = new Vector3(transform.position.x, f1jumping, transform.position.z);
        mOb.plusExp = 50;
        
    }
    private void Update()
    {
        Om om = Gv.gThis.mOm;
        mOb.time += Time.deltaTime;
        mOb.attacktime += Time.deltaTime; 
        oldPos = mOb.getPos();
        List<ObjBase> play = Gv.gThis.mOm.FindPlayer();
        emy.targertOb = (Player)play[0].mMb;
        Vector3 targetPos = play[0].getPos();
        Vector3 targetRad = play[0].getRadius();

        



        if (play.Count > 0)
        {
            Move(play,targetPos,targetRad);
                


            //if (targetPos.x - transform.position.x < 0.9 && targetPos.x - transform.position.x > -0.9&&
            //        targetPos.y-transform.position.y<0.9&&targetPos.y-transform.position.y>-0.9 && timese > 1.0f)


            
        }
    }
    private void Move(List<ObjBase>play, Vector3 targetPos,Vector3 targetRad)
    {
 
        if (Mathf.Abs(targetPos.x - transform.position.x) > targetRad.x)
        {

            mOb.righ = (targetPos.x > transform.position.x) ? "Right" : "Left";
            transform.position = new Vector3(transform.position.x + ((targetPos.x > transform.position.x) ? +Time.deltaTime : -Time.deltaTime) * emy.movespeed, transform.position.y, 0);

            mOb.ani.SetBool("Move", true);
        }
        else
        {

            
            mOb.ani.SetBool("Attack", true);
           
        }
    }
    private void Attack()
    {
        Debug.Log("공격");
        GameObject hitbox = mOb.GetHitBox();
        List<ObjBase> fos = mOb.Httest(hitbox);
        Debug.Log(hitbox.name);
        Debug.Log("숫자"+fos.Count);
        for (int i = 0; i < fos.Count; i++)
        {

            if (fos[i].mType == Player.gType)
            {
                hitbox.SetActive(true);

                mOb.Attack1(emy.targertOb.mOb);
                hitbox.SetActive(false);               
                
            }

        }
    }
    public void AttackEnd()
    {
        mOb.ani.SetBool("Attack",false);
    }
    private void FixedUpdate()
    {
        
        

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


