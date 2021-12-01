using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

//delete... where Score <= (select Score from (SELECT * FROM ScoreDB ORDER By Score ASC LIMIT (15-10)) ORDER By Score DESC LIMIT 1)
//delete... where Score in (select Score from (SELECT * FROM ScoreDB ORDER By Score ASC LIMIT (15-10))

public class TestDB : MonoBehaviour
{
    public string secet= "SELECT * FROM ScoreDB ORDER By Score DESC LIMIT 10;";
    public string Insert = "Insert Into ScoreDB(Name,Score) VALUES";
   
    private string Dbpath = "/Resources/ScoreDBT.db";
    public string delect = "DELETE FROM ScoreDB where Score in (select Score from(SELECT* FROM ScoreDB ORDER By Score ASC LIMIT (11-10)))";
    [SerializeField]
    private List<String> Pname = new List<string>();
    [SerializeField]
    private List<int> score = new List<int>();
    [SerializeField]
    private List<int> DbID = new List<int>();
    [SerializeField]
    private GameObject scrol;
    [SerializeField]
    private List<Text> PnameT = new List<Text>();
    [SerializeField]
    private List<Text> scoreT = new List<Text>();
    [SerializeField]
    private List<Text> DbIdT = new List<Text>();
    public int id = 0;
    IDbConnection dbConnection;


    IDbCommand dbCommand;
    public void Test()
    {
        for(int i = 0; i < score.Count; i++)
        {
            Debug.Log("자 들가자"+score[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DBconnetionCheck();
       
        DataBaseRead("SELECT * FROM ScoreDB ORDER By Score DESC");

    }
    private void Awake()
    {
        StartCoroutine(DBcreate());
       
        
       
    }
    public void DBRank()
    {
       
            scrol.SetActive((scrol.activeSelf ? false : true));

            for (int i = 0; i < id-1; i++)
            {
                DbIdT[i].text = (i + 1) + "등";
                PnameT[i].text = "이름:" + Pname[i];
                scoreT[i].text = "점수:" + score[i];
            }
            DataBaseRead(secet);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DBcreate()
    {
        string filepath = string.Empty;
        filepath = Application.dataPath + Dbpath;
        Debug.LogError("버그망겜");
        if (!File.Exists(filepath))
        {
            
            File.Copy(Application.streamingAssetsPath + Dbpath, filepath);
           
        }
        Debug.Log("성공?" + (Application.streamingAssetsPath + Dbpath, filepath));
        
         yield return null;
    }
    public string GetDBFilePath()
    {
        string str = string.Empty;
        str = "URI=file:" + Application.dataPath + Dbpath;
        return str;
    }
    public void DBconnetionCheck()
    {
        try
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }
          
        }
        catch(Exception e)
        {
           
            Debug.Log(e);
        }
    }
    public int DataBaseRead(string que)
    {
        ClearList();
        DataBaseStart();
        dbCommand.CommandText = que;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {

            Debug.Log(dataReader.GetInt32(0) + "," + dataReader.GetString(1) + "," + dataReader.GetInt32(2));
          
            DbID.Add(dataReader.GetInt32(3));
            score.Add(dataReader.GetInt32(2));
            Pname.Add(dataReader.GetString(1));
            id++;

        }
        dataReader.Dispose();
        dataReader = null;
        CloseDB();
        return id;

    }
    public void ClearList()
    {
        DbID.Clear();
        score.Clear();
        Pname.Clear();
        id = 1;
    }
    public void ClearListT()
    {
        for(int i = 0; i < DbIdT.Count; i++)
        {
            DbIdT[i].text = null;
            scoreT[i].text = null;
            PnameT[i].text = null;
        }
    }
    public string DataBaseInsert(string que)
    {
        DataBaseStart();
        dbCommand.CommandText = que;
        dbCommand.ExecuteNonQuery();
        CloseDB();
        return que;
    }
   
    public void DataBaseStart()
    {
        dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        dbCommand = dbConnection.CreateCommand();
    }
    public void CloseDB()
    {
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}
