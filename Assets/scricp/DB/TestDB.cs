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
    public int id = 0;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        DBconnetionCheck();
    }
    private void Awake()
    {
        StartCoroutine(DBcreate());
       
        
       
    }
    private void DBRank()
    {
        image.gameObject.SetActive(true);
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
        id = 0;
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = que;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {

            Debug.Log(dataReader.GetInt32(0));
            
            
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
        return id;

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
}
