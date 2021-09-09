using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emy2 : MonoBehaviour, ObjInterface
{
    public static string gType = "Enemy2";
    //  private List<ObjBase> play;
    private bool stop = false;
    private float timese;



    public ObjBase mOb;


    //public Vector3 getPos()
    //{
    //    return this.transform.position;
    //}

    void Start()
    {
        mOb = new ObjBase();
        mOb.mMb = this;
        mOb.mType = Emy2.gType;

        mOb.HitboxR = this.transform.Find("HItBoxRight").gameObject;
        mOb.HitboxR.SetActive(false);
        mOb.HitboxL = this.transform.Find("HItBoxLeft").gameObject;

        mOb.HitboxL.SetActive(false);
        Gv.gThis.mOm.Add(this.mOb);



    }
    private void Update()
    {

        timese += Time.deltaTime;
        mOb.attacktime += Time.deltaTime;
        List<ObjBase> play = Gv.gThis.mOm.FindPlayer();


        if (play.Count > 0)
        {
            Player targertOb = (Player)play[0].mMb;
            Vector3 targetPos = play[0].getPos();




            if (targetPos.x - transform.position.x > 0)
            {
                mOb.righ = "Right";
                transform.position = new Vector3(transform.position.x + Time.deltaTime * 0.9f, transform.position.y, 0);


            }
            if (targetPos.x - transform.position.x < 0)
            {
                mOb.righ = "Left";
                transform.position = new Vector3(transform.position.x - Time.deltaTime*0.9f, transform.position.y, 0);
            }


            //if (targetPos.x - transform.position.x < 0.9 && targetPos.x - transform.position.x > -0.9&&
            //        targetPos.y-transform.position.y<0.9&&targetPos.y-transform.position.y>-0.9 && timese > 1.0f)


            if (mOb.attacktime > 1.0f)
            {

                if (targetPos.x - this.transform.position.x < 0.05 && targetPos.x - this.transform.position.x > -0.05 &&
                    targetPos.y - this.transform.position.y < 0.05f && targetPos.y - this.transform.position.y > -0.05)
                {
                    if (mOb.righ == "Right")
                    {
                        targertOb.transform.position = new Vector3(targertOb.transform.position.x + 1f, targertOb.transform.position.y, targertOb.transform.position.z);
                    }
                    else if (mOb.righ == "Left")
                    {
                        targertOb.transform.position = new Vector3(targertOb.transform.position.x - 1f, targertOb.transform.position.y, targertOb.transform.position.z);
                    }
                    Debug.Log("성");
                    timese = 0;
                    mOb.attacktime = 0;
                    targertOb.wait = true;
                    Debug.Log(targertOb.mOb);
                    mOb.Attack1(targertOb.mOb);


                }
                if (mOb.righ == "Left")
                {
                    if (targetPos.x - mOb.HitboxL.transform.position.x > -0.5f && targetPos.y - mOb.HitboxL.transform.position.y < 0.5f && targetPos.y - mOb.HitboxL.transform.position.y > -0.5f)
                    {

                        mOb.HitboxL.SetActive(true);

                        //Debug.Log(" " + mOb.mMb);
                        mOb.Attack1(targertOb.mOb);

                        timese = 0;
                        mOb.attacktime = 0;
                    }
                }
                if (mOb.righ == "Right")
                {
                    if (targetPos.x - mOb.HitboxR.transform.position.x < 0.5f && targetPos.y - mOb.HitboxR.transform.position.y < 0.5f && targetPos.y - mOb.HitboxR.transform.position.y > -0.5f)
                    {

                        mOb.HitboxR.SetActive(true);


                        mOb.Attack1(targertOb.mOb);
                        timese = 0;
                        mOb.attacktime = 0;
                    }
                }
                stop = true;



            }









        }




    }
    private void FixedUpdate()
    {
        if (timese > 0.2f)
        {
            mOb.HitboxR.SetActive(false);
            mOb.HitboxL.SetActive(false);
            timese = 0;
        }

    }
}
