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
    [SerializeField]
    private int emyLe;
    [SerializeField]
    public int score = 0;


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
                    player.mOb.curhp = 50;
                    player.mOb.maxhp = 500;
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
                                LoadOb(ems[i], maxemy,i);
                                break;
                            case 1:
                               maxemy = 1;
                               LoadOb(ems[i], maxemy,i);
                                break;
                            case 2:
                                maxemy = 1;
                                LoadOb(ems[i], maxemy,i);
                               
                                break;
                            case 3:
                                maxemy = 1;
                                LoadOb(ems[i], maxemy,i);

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
   private void LoadOb(GameObject _ems,int maxemy,int id)
    {
         emyLe=(int)GameObject.FindGameObjectsWithTag("Emy").Length;
        if (emyLe < 100)
        {

            for (int i = 0; i < maxemy; i++)
            {

                GameObject obj = Instantiate(_ems);
                AttackOB(id, obj);
                obj.transform.position = new Vector3(resofomL.position.x + i, resofomL.position.y);
                GameObject obj2 = Instantiate(_ems);
                AttackOB(id, obj2);
                obj2.transform.position = new Vector3(resofomR.position.x + i, resofomR.position.y);


            }
        }
      
    }
    private void AttackOB(int id,GameObject obj)
    {
        switch (id)
        {
            case 0:
                Debug.Log("앙"+ obj.GetComponent<Emy1>().mOb);
                obj.GetComponent<Emy1>().mOb.attackp += 100;
                break;
            case 1:
                obj.GetComponent<Emy2>().mOb.attackp += 400;
                break;
            case 2:obj.GetComponent<Emy3>().mOb.attackp += 500;
                break;
            case 3:
                obj.GetComponent<Emy4>().mOb.attackp += 1000;
                break;

        }
    }
    public void LeveUpEf()
    {
        GameObject obj = Instantiate(levelUp);
        obj.transform.position = player.transform.position;
    }
    public void ScoreM(int _score)
    {
        score += _score;    
    }
    public int ScoreT()
    {
        return score;
    }
}
