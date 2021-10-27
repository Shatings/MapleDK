using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summons : MonoBehaviour
{
    [SerializeField]
    public float deatime;
    public ObjBase mOb;
    [SerializeField]
    private float attackTIme = 0f;
    public static string gType = "Summons";


    // Start is called before the first frame update
    void Start()
    {
        mOb = new ObjBase();
        mOb.mType = gType;
        
        mOb.attackp = 50;
        mOb.speed = 10;
        
        mOb.Hitbox = this.transform.Find("HItBoxRight").gameObject;
        mOb.HitboxSkill = this.transform.Find("HitCheck").gameObject;
        Invoke("DestroyOb", deatime);
    }

    // Update is called once per frame
    void Update()
    {
        Om om = Gv.gThis.mOm;
        List<ObjBase> play = Gv.gThis.mOm.FindPlayer();
        
        Move(play[0]);
        
        
    }
    private void FixedUpdate()
    {
        if (attackTIme !=0)
        {
            attackTIme += Time.deltaTime;
            if (attackTIme > 0.2f)
            {
                attackTIme = 0;
                mOb.Hitbox.SetActive(true);
            }
            else
            {
                mOb.Hitbox.SetActive(false);
            }
           
        }
    }
    private void Move(ObjBase play)
    {
       
        if (Check() == null)
        {
            this.transform.localScale = new Vector3(play.transform.localScale.x, transform.localScale.y, transform.localScale.z);
            this.transform.position = new Vector3(transform.position.x + (play.transform.position.x - transform.position.x > 0 ? +Time.deltaTime * mOb.speed : -Time.deltaTime * mOb.speed), play.transform.position.y + 1);
            
        }
        else
        {
            
            ObjBase target = Check();
            Debug.Log("타켓 타입"+target.mType);
            Vector3 targetPos = target.getPos();
            Vector3 targetRad = target.getRadius();

            Debug.Log("결과"+(Mathf.Abs(targetPos.x - transform.position.x) >0));
            Debug.Log("타겟 위치-현재위치:" + (targetPos.x - transform.position.x) + "Rand:" + targetRad.x);
            if (Mathf.Abs(targetPos.x - transform.position.x) >0)
            {

                transform.localScale = (targetPos.x > transform.position.x) ? new Vector3(-1, transform.localScale.y, transform.localScale.z) : new Vector3(1, transform.localScale.y, transform.localScale.z);
                transform.position = new Vector3(transform.position.x + ((targetPos.x > transform.position.x) ? +Time.deltaTime : -Time.deltaTime) * mOb.speed, play.transform.position.y + 1, 0);
                if (attackTIme == 0)
                {
                    Attack();
                    attackTIme += Time.deltaTime;
                }
                

            }
            

        }
    }
    private void Attack()
    {
        GameObject hitbox = mOb.GetHitBox();
        List<ObjBase> fos = mOb.Httest(hitbox);

        for (int i = 0; i < fos.Count; i++)
        {
            Debug.Log("공격중임");
            if (fos[i].mType == Emy1.gType || fos[i].mType == Emy2.gType || fos[i].mType == Emy3.gType || fos[i].mType == Emy4.gType)
            {
                mOb.Attack1(fos[i], this.gameObject);
            }

        }
    }
    private ObjBase Check()
    {
        List<ObjBase>fos=mOb.Httest(mOb.HitboxSkill);
        for(int i = 0; i < fos.Count; i++)
        {
            if (fos[i].mType == Emy1.gType || fos[i].mType == Emy2.gType || fos[i].mType == Emy3.gType || fos[i].mType == Emy4.gType)
            {
                return fos[i];
            }
        }
        return null;
    }
    
    private void DestroyOb()
    {
        Destroy(gameObject);
    }
}
