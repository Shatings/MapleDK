using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMgr : MonoBehaviour
{
    
    private GameObject ems;
    private GameObject ems2;
    private GameObject levelUp;
    private float timea;
    private bool Stop=false;
    private Player player;
    public int mGameState;
    public Item items;

    private string reso = "Prefab/";
    [SerializeField]
    private Transform resofom;
    


    void Start()
    {
        ems = Resources.Load(""+reso+Emy1.gType) as GameObject;
        Debug.Log(reso + Emy1.gType);
        ems2 = Resources.Load(reso + Emy2.gType) as GameObject;
        items = Resources.Load("items/Item1") as Item;
        levelUp = Resources.Load("" + reso + "LevelUpEf") as GameObject;

        //boss = Resources.Load("Prefab/Boss1") as GameObject;
        player = FindObjectOfType<Player>();
        mGameState = 0;
    }
    public Item Radndom()
    {
        int random = Random.Range(1, 5);

        items = Resources.Load("items/Item" + random.ToString()) as Item;
        var item = Instantiate(items);
        Debug.Log(" "+items.id);
        
        return item ;
    }
    // Update is called once per frame
    void Update()
    {
        timea += Time.deltaTime;
        Om om = Gv.gThis.mOm;
        
        switch (mGameState)
        {
            case  0: //Start


                //db load
                {
                    player.mOb.curhp = 5000;
                    player.mOb.maxhp = 5000;
                    player.mOb.attackp = 100;
                    player.jumppower = 4f;
                    player.level = 1;
                    player.curexp = 0;
                    player.maxexp = 100;
                    


                    //enemy.attackxxx = 123;
                    // ....
                    ///
                }

                mGameState = 1;
                break;

            case 1: //Play
                StartCoroutine(LoadOb(ems));
                mGameState++;
                break;
            case 2:
                if (player.dead >= 4)
                {
                    StartCoroutine(LoadOb(ems2));
                    mGameState++;
                }

                break;
        }
    }
   IEnumerator LoadOb(GameObject _ems)
    {
        
        
        for (int i = 0; i <= 4; i++)
        {
           
            GameObject obj = Instantiate(_ems);
            obj.transform.position = new Vector3(resofom.position.x+i,resofom.position.y);

            
        }
        yield return null;
    }
    public void LeveUpEf()
    {
        GameObject obj = Instantiate(levelUp);
        obj.transform.position = player.transform.position;
    }
}
