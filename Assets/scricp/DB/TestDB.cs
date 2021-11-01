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
    public string udate=null;
    private string Dbpath = "/ScoreDBT.db";
    public string delect = "Delect From ScoreDB Where ID>10";
    [SerializeField]
    private List<String> Pname = new List<string>();
    [SerializeField]
    private List<int> score = new List<int>();
    [SerializeField]
    private List<int> DbID = new List<int>();
    
    public int id = 0;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text text;
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
        image.gameObject.SetActive(true);
        text.text = null;
        for(int i = 0; i < id; i++)
        {
            text.text +=(i+1)+"등 이름:" + Pname[i] + "점수:" + score[i]+"\n";
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
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
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
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
        return id;

    }
    public void ClearList()
    {
        DbID.Clear();
        score.Clear();
        Pname.Clear();
        id = 1;
    }
    public void DataBaseInsert(string que)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = que;
        dbCommand.ExecuteNonQuery();
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
    public void DataUpdate()
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        for(int i = 1; i < id; i++)
        {

            udate = "UPDATE ScoreDB SET Name = \"" + Pname[i-1] + "\",Score = "+score[i-1]+","+"ID="+i+" WHERE ID ="+i;
            Debug.Log("엄"+ udate);
            dbCommand.CommandText = udate;
            dbCommand.ExecuteNonQuery();
        }
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
        DateBaseDelect(delect);
    }
    public void DateBaseDelect(string que)
    {
        Debug.Log("히히"+que);
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = que;
        dbCommand.ExecuteNonQuery();
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}
