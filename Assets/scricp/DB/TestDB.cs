using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class TestDB : MonoBehaviour
{
    public string secet="Select * From Score ORDER BY Score DESC";
    public string Insert = "Insert Into Score(Name,Score) VALUES";
    private string Dbpath = "/ScoreDB.db";
    public string delect = "Delect From Score Where Id>10";
    [SerializeField]
    private List<String> Pname = new List<string>();
    [SerializeField]
    private List<int> score = new List<int>();
    
    public int id = 0;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        DBconnetionCheck();
        DataBaseRead(secet);
    }
    private void Awake()
    {
        StartCoroutine(DBcreate());
       
        
       
    }
    public void DBRank()
    {
        image.gameObject.SetActive(true);
        text.text = null;
        for(int i = 0; i < score.Count; i++)
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
    public void DataInsertT(int Score)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        for(int i = 1; i <= id; i++)
        {
            if (Score > score[i])
            {
                Insert+= "(\"" + Player.gType + "\"," + Score+ ")";
                Debug.Log("엄"+Insert);
                DataBaseInsert(Insert);
            }
            else
            {

            }
        }
    }
    public void DateBaseDelect(string que)
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
}
