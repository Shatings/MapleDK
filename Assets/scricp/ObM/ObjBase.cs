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
    public GameObject Hitbox;
    public GameObject HitboxSkill;
    public string righ;
    public int maxhp = 100;
    public int curhp = 100;
    public int attackp = 50;
    public float speed = 5f;
    public float attacktime;
    public bool Edie = false;
    public int monsternumber;
    public float time;
    public int plusExp;
    public bool die=false;
    
    public Animator ani;

    public float floor1y=-0.2f;
    public SpriteRenderer sprite;
    public float attackz = -0.01f;
    public float adimz = 0;
    public float basicspeed= 5f;
    public AudioClip clip;
    public int point;
    public Transform transform;
    public float Aps = 0;





    public ObjBase()
    {
        mType = "None";
        
    }
    public void DangeText( ObjBase player)
    {
        Transform tarD = player.transform;

        GameObject hitT = GameObject.Instantiate(Resources.Load("Prefab/DangeT") as GameObject);
        hitT.GetComponent<HitM>().DangeT(player);
       
        hitT.transform.position = tarD.position;
        
    }

    public Vector3 getPos()
    {
        return mMb.transform.position;
    }
    public Vector3 getRadius()
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

    public void Attack1(ObjBase tarob,GameObject gameOb)
    {
        //throw new System.NotImplementedException();
        Vector3 tran = tarob.mMb.gameObject.transform.position;
       
        tarob.curhp -= attackp;
        Debug.Log(" " + tarob.mType);
      
        tarob.ani.SetBool("Hit", true);
        gameOb.transform.position=new Vector3(gameOb.transform.position.x,gameOb.transform.position.y,attackz);
        DangeText(tarob);
        
        
        tarob.mMb.gameObject.transform.position = new Vector3(tran.x, tran.y, adimz);
        if (tarob.mType == Player.gType)
        {
            GameObject.FindObjectOfType<Player>().inv = true;
        }
        if (tarob.curhp <= 0)
        {
            try
            {
                if (tarob.mType == Emy1.gType || tarob.mType == Emy2.gType || tarob.mType == Emy3.gType || tarob.mType == Emy4.gType)
                {
                    PlusExp(tarob);
                    tarob.die = true;
                    Object.FindObjectOfType<Player>().dead++;
                    Object.FindObjectOfType<GameMgr>().ScoreM(tarob.point);


                }
                Debug.Log(tarob.mType + " Dead");
                tarob.ani.SetBool("Die", true);
              


                GameObject.Find("Canvas").transform.Find("Scroll View").transform.Find("Viewport").GetComponent<Invetory>().AddItem(GameObject.Find("GameMgr").GetComponent<GameMgr>().Radndom());
                Gv.gThis.mOm.Remove(tarob);
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
            }

        }
    }
    public void Die(GameObject _gameObject)
    {
        GameObject.Destroy(_gameObject);
    }
    public void AttackEnd(ObjBase mOb)
    {
        mOb.ani.SetBool("Attack", false);
       


    }
    public void HitEnd(ObjBase mOb)
    {
        mOb.ani.SetBool("Hit", false);
    }

    public void PlusExp(ObjBase tarob)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.curexp += tarob.plusExp;
        
       
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
        //player.mOb.maxhp += 1000;
        //player.mOb.curhp = player.mOb.maxhp;
        GameObject.Find("GameMgr").GetComponent<GameMgr>().LeveUpEf();
    }
    
    public GameObject GetHitBox()
    {
        Om om = Gv.gThis.mOm;
        
        GameObject hitbox = Hitbox;
        if (righ == "Right")
        {

            hitbox = Hitbox;

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
   





}
