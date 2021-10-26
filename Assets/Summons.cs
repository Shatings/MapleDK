using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summons : MonoBehaviour
{
    [SerializeField]
    private float deatime;
    public ObjBase mOb;

    // Start is called before the first frame update
    void Start()
    {
        mOb = new ObjBase();
        mOb.attackp = 200;
        mOb.speed = 100;
        
        mOb.Hitbox = this.transform.Find("HItBoxRight").gameObject;
        Invoke("DestroyOb", deatime);
    }

    // Update is called once per frame
    void Update()
    {
        Om om = Gv.gThis.mOm;
        List<ObjBase> play = Gv.gThis.mOm.FindPlayer();
        Move(play[0]);
        Attack();
        
    }
    private void Move(ObjBase play)
    {
        
        this.transform.position = new Vector3(transform.position.x+ (play.transform.position.x - transform.position.x>0 ?+Time.deltaTime*mOb.speed :- Time.deltaTime * mOb.speed),play.transform.position.y + 1);
        this.transform.localScale = new Vector3(play.transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
    
    private void DestroyOb()
    {
        Destroy(gameObject);
    }
}
