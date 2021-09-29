using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMgr : MonoBehaviour
{
    
    private GameObject ems;
    private GameObject ems2;
    private float timea;
    private bool Stop=false;
    private Player player;
    public int mGameState;
    public Item items;
    


    void Start()
    {
        ems = Resources.Load("Prefab/Emy") as GameObject;
        ems2 = Resources.Load("Prefab/Emy2") as GameObject;
        items = Resources.Load("items/Item1") as Item;

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
        float ranx = Random.Range(-30, 50);
        float rany = -0.52f;
        switch (mGameState)
        {
            case  0: //Start


                //db load
                {
                    player.mOb.curhp = 5000;
                    player.mOb.maxhp = 5000;
                    player.mOb.attackp = 100;
                    player.jumppower = 0.01f;
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

                
                break;
        }
        timea += Time.deltaTime;
        Om om = Gv.gThis.mOm;
        Debug.Log("카운트 " + om.mOs.Count);
        if (om.mOs.Count < 10)
        {
            Debug.Log("1");
            GameObject obj = Instantiate(ems);
        

            obj.transform.position = new Vector3(ranx, rany, 0);

            //boss
            //obj.ob.mType = "Boss1";


        }
        
        if (player.dead >= 4 && !Stop)
        {
            Debug.Log("성");
            Stop = true;

        }




    }
}
