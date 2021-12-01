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
    [SerializeField]
    private bool checkDb=false;
    [SerializeField]
    private float realTime;

    public GameObject ese;

    public void CheckHp()
    {
        if (player.mOb.curhp <= 0 && !checkDb)
        {
            checkDb = true;
            TestDB test = FindObjectOfType<TestDB>();
            //Debug.Log("아이디이다" + test.DataBaseRead("SELECT * FROM Score"));
            test.DataBaseInsert("Insert Into ScoreDB(Name, Score,ID) VALUES" + "(" + "\"" + Player.gType + "\"," + score + "," + test.id + ")");
            test.DataBaseInsert(test.delect);
            test.Test();
            test.DataBaseRead(test.secet);
            test.Test();
            
            test.DBRank();
        }

            
        
    }
    void Start()
    {
        
        
        for (int i = 0; i < 4; i++)
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
    public void ESE()
    {
        ese.SetActive((ese.activeSelf ? false : true));
    }
    // Update is called once per frame
    void Update()
    {
        
        Om om = Gv.gThis.mOm;
        realTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESE();
        }
        switch (mGameState)
        {
            case  0: //Start
                //db load
                {
                    player.mOb.curhp = 10;
                    player.mOb.maxhp = 10;
                    player.mOb.attackp = 1;
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
                        if (checkretime[i] >= lastretime[i])
                        {
                            checkretime[i] -=2f;
                        }
                        
                           
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
    public void ScoreM(int _score)
    {
        score += _score;    
    }
    public int ScoreT()
    {
        return score;
    }
}
