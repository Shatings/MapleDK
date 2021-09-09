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
        
        mOb.HitboxR = this.transform.Find("HItBoxRight").gameObject;
        mOb.HitboxR.SetActive(false);
        mOb.HitboxL = this.transform.Find("HItBoxLeft").gameObject;
        
        mOb.HitboxL.SetActive(false);
        Gv.gThis.mOm.Add(this.mOb);

        f1jumping = -0.52f;
        oldPos = mOb.getPos();
        transform.position = new Vector3(transform.position.x, oldPos.y, transform.position.z);
        
    }
    private void Update()
    {
        Om om = Gv.gThis.mOm;
        mOb.time += Time.deltaTime;
        mOb.attacktime += Time.deltaTime; 
        oldPos = mOb.getPos();
        List<ObjBase> play = Gv.gThis.mOm.FindPlayer();

        if (play.Count > 0)
        {
            emy.targertOb = (Player)play[0].mMb;
            Vector3 targetPos =play[0].getPos();



            
                if (targetPos.x - transform.position.x >= 0)
                {
                    mOb.righ = "Right";
                    transform.position = new Vector3(transform.position.x + Time.deltaTime*emy.movespeed, transform.position.y, 0);


                }
                if (targetPos.x - transform.position.x <=0)
                {
                    mOb.righ = "Left";
                    transform.position = new Vector3(transform.position.x - Time.deltaTime*emy.movespeed, transform.position.y, 0);
                }
                if (targetPos.x - transform.position.x ==0)
                {
                
                transform.position =  vector* emy.movespeed * Time.deltaTime;
                }


            //if (targetPos.x - transform.position.x < 0.9 && targetPos.x - transform.position.x > -0.9&&
            //        targetPos.y-transform.position.y<0.9&&targetPos.y-transform.position.y>-0.9 && timese > 1.0f)


            if (mOb.attacktime > 1.0f)
            {
                Debug.Log("공격");
                GameObject hitbox = mOb.GetHitBox();
                List<ObjBase> fos = mOb.Httest(hitbox);

                for (int i = 0; i < fos.Count; i++)
                {

                    if (fos[i].mType == Player.gType)
                    {
                        hitbox.SetActive(true);
                        mOb.Attack1(emy.targertOb.mOb);
                        mOb.time = 0;
                        mOb.attacktime = 0;
                    }

                }

            }



          



        }




    }
    private void FixedUpdate()
    {
        if (mOb.time > 0.2f)
        {
            mOb.HitboxR.SetActive(false);
            mOb.HitboxL.SetActive(false);
            mOb.time = 0;
        }

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


