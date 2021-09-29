using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjInterface
{
    
   // void OnAttack(MonoBehaviour mb);
    //Vector3 getPos();
}
public class ObjBase 
{
  
    public MonoBehaviour mMb;
    public string mType;
    public ObjInterface mOi;
    public GameObject HitboxR;
    public GameObject HitboxL;
    public string righ;
    public int maxhp = 100;
    public int curhp = 100;
    public int attackp = 50;
    public float attacktime;
    public bool Edie = false;
    public int monsternumber;
    public float time;
    public int plusExp;
    


    public ObjBase()
    {
        mType = "None";
    }


    public Vector3 getPos()
    {
        return mMb.transform.position;
    }
    public Vector3 getTest()
    {
        return mMb.transform.localScale;
    }
    public Vector3 getFoot()
    {
        return getFoot(getPos());
    }
    public Vector3 getFoot(Vector3 pos )
    {
        return pos - new Vector3(0, 0.5f, 0);
    }

    public void Attack1(ObjBase tarob)
    {
        //throw new System.NotImplementedException();
        
           

            tarob.curhp -= attackp;
            Debug.Log(tarob.mType + " Hit " + attackp);
            
            if (tarob.curhp <= 0)
            {
            if (tarob.mType == Emy1.gType)
            {
                PlusExp(tarob);
            }
                Debug.Log(tarob.mType+" Dead");
               
                GameObject.Destroy(tarob.mMb.gameObject);
                
                GameObject.Find("Canvas").transform.Find("Scroll View").transform.Find("Viewport").GetComponent<Invetory>().AddItem(GameObject.Find("GameMgr").GetComponent<GameMgr>().Radndom());
                Gv.gThis.mOm.Remove(tarob);


            }

        



    }
    public void PlusExp(ObjBase tarob)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.curexp += tarob.plusExp;
        if (player.curexp >= player.maxexp)
        {
            player.level += player.curexp / player.maxexp;
            player.curexp = player.curexp % player.maxexp;
            PlusLevel(player);
        }
       
    }
    public void PlusLevel(Player player)
    {
        switch (player.level) 
        {
            case 1:
                player.maxexp += 10;
                break;
            case 2:
            case 3:
            case 4:
                player.maxexp += 200;
                break;
            case 5:
            case 6:
            case 7:
                player.maxexp += 300;
                break;
            case 8:
            case 9:
            case 10:
                player.maxexp += 500;
                break;

        }
    }
    public void HtBox()
    {
        Om om = Gv.gThis.mOm;
        List<ObjBase> fos;
        GameObject hitbox = HitboxL;
        if (righ == "Right")
        {

            hitbox = HitboxR;

        }
        fos = om.findPos(hitbox.transform.position.x, hitbox.transform.position.y, hitbox.transform.localScale.x, hitbox.transform.localScale.y);


        for (int i = 0; i < fos.Count; i++)
        {

            if (fos[i].mType == Emy1.gType || fos[i].mType == Emy2.gType||fos[i].mType==Player.gType)
            {
                Attack1(fos[i]);
               

            }


        }
       
        
        
    }
    public GameObject GetHitBox()
    {
        Om om = Gv.gThis.mOm;
        
        GameObject hitbox = HitboxL;
        if (righ == "Right")
        {

            hitbox = HitboxR;

        }
       


        return hitbox;



    }
    public List<ObjBase> Httest(GameObject hitbox)
    {
        Om om = Gv.gThis.mOm;
        List<ObjBase> fos;
        
        fos = om.findPos(hitbox.transform.position.x, hitbox.transform.position.y, hitbox.transform.localScale.x, hitbox.transform.localScale.y);


        return fos;



    }
   




    //public void attack(int attack, List<ObjBase> fos)
    //{


    //    for (int i = 0; i < fos.Count; i++)
    //    {

    //        MonoBehaviour mb = fos[i];
    //        if (mb.name == "Emy" || mb.name == "Emy(Clone)")
    //        {

    //            Emy1 es = (Emy1)mb;
    //            es.curhp -= attack;
    //            Debug.Log(mb.name + " Hit " + attack);
    //            if (es.curhp <= 0)
    //            {
    //                Debug.Log("Dead");

    //                Destroy(es.gameObject);
    //                Gv.gThis.mOm.Remove(es);
    //                dead++;
    //                Debug.Log(" " + dead);
    //            }

    //        }


    //    }





    //}
    //private void PyAttck(int attackpower, List<ObjBase> fos)
    //{




    //    for (int i = 0; i < fos.Count; i++)
    //    {

    //        MonoBehaviour pl = fos[i];


    //        if (pl.name == "Player")
    //        {

    //            Player pe = (Player)pl;


    //            pe.Stat.curhp -= attackpower;
    //            Debug.Log(pl.name + " Hit " + attackpower);

    //            if (pe.Stat.curhp <= 0)
    //            {
    //                Debug.Log("Dead");

    //                Destroy(pe.gameObject);
    //                Gv.gThis.mOm.Remove(pe);
    //            }





    //        }


    //    }


    //}
}
