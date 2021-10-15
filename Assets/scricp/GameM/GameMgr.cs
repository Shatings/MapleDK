using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMgr : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ems=new List<GameObject>();
    private GameObject ems2;
    private GameObject levelUp;
   
    private bool Stop=false;
    private Player player;
    public int mGameState;
    public Item items;

    private string reso = "Prefab/";
    [SerializeField]
    private Transform resofomL;
    [SerializeField]
    private Transform resofomR;
    private int maxemy;
    [SerializeField]
    private List<float> resoformtime = new List<float>();
    [SerializeField]
    private List<float> checkretime = new List<float>();
    [SerializeField]
    private List<float> lastretime = new List<float>();


    void Start()
    {
       
        for(int i = 0; i < 4; i++)
        {
            ems.Add(Resources.Load("" + reso + "Enemy"+(i+1).ToString()) as GameObject);
        }
        
        Debug.Log(reso + Emy1.gType);
        ems2 = Resources.Load(""+reso + Emy2.gType) as GameObject;
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
                    player.curexp = 1;
                    player.maxexp = 100;
                }

                mGameState = 1;
                break;

            case 1: //Play
                for(int i = 0; i < resoformtime.Count; i++)
                {
                    resoformtime[i] += Time.deltaTime;
                    if (resoformtime[i] > checkretime[i])
                    {
                       
                        switch (i)
                        {
                            case 0:
                                maxemy = 1;
                                LoadOb(ems[i], maxemy);
                                break;
                            case 1:
                               maxemy = 1;
                               LoadOb(ems[i], maxemy);
                                break;
                            case 2:
                                maxemy = 1;
                                LoadOb(ems[i], maxemy);
                                Debug.Log("아직 미구현");
                                break;
                            case 3:
                                
                                break;
                            default:
                                Debug.Log("엄준식엄준식 신나는노래");
                                break;
                                
                        }
                        
                        resoformtime[i] = 0;
                        if (checkretime[i] <= lastretime[i])
                        {
                            return;
                        }

                        checkretime[i] -= 2;


                    }
                }
                
                
                
                
                break;
           
        }
    }
   private void LoadOb(GameObject _ems,int maxemy)
    {
        int emyLe=(int)GameObject.FindGameObjectsWithTag("Emy").Length;
        if (emyLe < 100)
        {

            for (int i = 0; i < maxemy; i++)
            {

                GameObject obj = Instantiate(_ems);
                obj.transform.position = new Vector3(resofomL.position.x + i, resofomL.position.y);
                GameObject obj2 = Instantiate(_ems);
                obj2.transform.position = new Vector3(resofomR.position.x + i, resofomR.position.y);


            }
        }
      
    }
    public void LeveUpEf()
    {
        GameObject obj = Instantiate(levelUp);
        obj.transform.position = player.transform.position;
    }
}
