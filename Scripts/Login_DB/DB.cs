using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;

public class DB : MonoBehaviour
{
    public static SqliteConnection Connection;
    private static string path;

    private void OnEnable()
    {
        ScoreManager.ScoreDataUpdated += UpdateRecord;
    }

    private void OnDisable()
    {
        ScoreManager.ScoreDataUpdated -= UpdateRecord;
    }

    public static string CurrentUser { set { PlayerPrefs.SetString("_currentUser", value); } }

    public static void InsertNewUser(string name, string username, string password)
    {
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);
        string userCommandText = String.Format("INSERT INTO Users VALUES ('{0}', '{1}', '{2}')", username, name, password);
        string scoresCommandText = String.Format("INSERT INTO Scores(user_username, level, best_score, best_time)" +
                                    "VALUES('{0}', 1, 0, '00:00')," +
                                    "('{0}', 2, 0, '00:00')," +
                                    "('{0}', 3, 0, '00:00')", username);
        string coinsAndSkinsCommandText = String.Format("INSERT INTO CoinsAndSkins (user_username, coins) VALUES ('{0}', 0)", username);

        Connection.Open();

        SqliteCommand insertUserCom = new SqliteCommand();
        insertUserCom.Connection = Connection;
        insertUserCom.CommandText = userCommandText;
        SqliteDataReader userReader = insertUserCom.ExecuteReader();

        SqliteCommand insertScoresCom = new SqliteCommand();
        insertScoresCom.Connection = Connection;
        insertScoresCom.CommandText = scoresCommandText;
        SqliteDataReader ScoreReader = insertScoresCom.ExecuteReader();

        SqliteCommand insertCoinsCom = new SqliteCommand();
        insertCoinsCom.Connection = Connection;
        insertCoinsCom.CommandText = coinsAndSkinsCommandText;
        SqliteDataReader coinsReader = insertCoinsCom.ExecuteReader();

        Connection.Close();
    }

    public static bool IsUsernameFree(string username)
    {
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);

        bool result = true;
        string selectText = "SELECT username from Users";

        Connection.Open();
        SqliteCommand selectCom = new SqliteCommand();
        selectCom.Connection = Connection;
        selectCom.CommandText = selectText;
        SqliteDataReader reader = selectCom.ExecuteReader();

        while (reader.Read())
        {
            if (reader[0].ToString() == username)
            {
                result = false;
                break;
            }
        }

        Connection.Close();
        return result;
    }

    public static bool IsPasswordCorrect(string username, string password)
    {
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);

        bool result = false;
        string selectText = String.Format("SELECT password from Users WHERE username = '{0}'", username);

        Connection.Open();
        SqliteCommand selectCom = new SqliteCommand();
        selectCom.Connection = Connection;
        selectCom.CommandText = selectText;
        SqliteDataReader reader = selectCom.ExecuteReader();

        while(reader.Read())
        {
            if (reader[0].ToString() == password)
            {
                result = true;
            }
        }

        Connection.Close();
        return result;
    }

    public static string GetUserName()
    {
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);

        string result = "";
        string selectText = String.Format("SELECT name FROM Users WHERE username = '{0}'", PlayerPrefs.GetString("_currentUser"));

        Connection.Open(); 
        SqliteCommand selectCom = new SqliteCommand();
        selectCom.Connection = Connection;
        selectCom.CommandText = selectText;
        SqliteDataReader reader = selectCom.ExecuteReader();

        while (reader.Read())
        {
            result = reader[0].ToString();
        }

        Connection.Close();
        return result;
    }

    // call in ScoreManager
    public static Dictionary<string, object[]> GetUserScores()
    {
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);

        string selectText = String.Format("SELECT level, best_time, best_score from Scores WHERE user_username = '{0}'", PlayerPrefs.GetString("_currentUser"));
        var result = new Dictionary<string, object[]>();

        result.Add("scores", new object[3]);
        result.Add("times", new object[3]);

        Connection.Open();
        SqliteCommand selectCom = new SqliteCommand();
        selectCom.Connection = Connection;
        selectCom.CommandText = selectText;
        SqliteDataReader reader = selectCom.ExecuteReader();

        while (reader.Read())
        {
            result["times"][int.Parse(reader[0].ToString()) - 1] = reader[1];
            result["scores"][int.Parse(reader[0].ToString()) - 1] = reader[2];
        }

        Connection.Close();
        return result;
    }

    // call in Log_in
    public static int GetCoins()
    {
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);

        int result = 0;
        string selectText = String.Format("SELECT coins FROM CoinsAndSkins WHERE user_username = '{0}'", PlayerPrefs.GetString("_currentUser"));

        Connection.Open();
        SqliteCommand selectCom = new SqliteCommand();
        selectCom.Connection = Connection;
        selectCom.CommandText = selectText;
        SqliteDataReader reader = selectCom.ExecuteReader();

        while (reader.Read())
        {
            result = int.Parse(reader[0].ToString());
        }

        Connection.Close();
        return result;
    }

    // call in Shop
    public static int[] GetSkinStates()
    {
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);

        int[] result = new int[12];
        string selectText = String.Format("SELECT s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12 FROM CoinsAndSkins WHERE user_username = '{0}'", PlayerPrefs.GetString("_currentUser"));

        Connection.Open();
        SqliteCommand selectCom = new SqliteCommand();
        selectCom.Connection = Connection;
        selectCom.CommandText = selectText;
        SqliteDataReader reader = selectCom.ExecuteReader();

        while (reader.Read())
        {
            for (int i = 0; i < 12; i++)
            {
                result[i] = Convert.ToInt32(reader[i]);             
            }
        }

        Connection.Close();
        return result;
    }

    public static void UpdateRecord(int level, string bestTime, int bestScore)
    {
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);

        string updateText = String.Format("UPDATE Scores SET best_time = '{0}', best_score = {1} " +
                                            "WHERE user_username = '{2}' AND level = {3}", 
                                            bestTime, bestScore.ToString(), PlayerPrefs.GetString("_currentUser"), 
                                            level.ToString());

        Connection.Open();
        SqliteCommand updateCom = new SqliteCommand();
        updateCom.Connection = Connection;
        updateCom.CommandText = updateText;
        SqliteDataReader reader = updateCom.ExecuteReader();

        Connection.Close();
    }

    // call in Coins
    public static void UpdateCoinRecord()
    {
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);

        string updateText = String.Format("UPDATE CoinsAndSkins SET coins = {0} " +
                                            "WHERE user_username = '{1}'", Coins.CoinCount, PlayerPrefs.GetString("_currentUser"));

        Connection.Open();
        SqliteCommand updateCom = new SqliteCommand();
        updateCom.Connection = Connection;
        updateCom.CommandText = updateText;
        SqliteDataReader reader = updateCom.ExecuteReader();
        Connection.Close();
    }

    // coll in Shop
    public static void UpdateSkinRecord(int skinIndex)
    {
        if (skinIndex > 13 || skinIndex < 1) return;
        path = Application.dataPath + "/StreamingAssets/UsersDB.bytes";
        Connection = new SqliteConnection("URI=file:" + path);

        string updateText = String.Format("UPDATE CoinsAndSkins SET s" + skinIndex + " = 1 " +
                                            "WHERE user_username = '{0}'", PlayerPrefs.GetString("_currentUser"));

        Connection.Open();
        SqliteCommand updateCom = new SqliteCommand();
        updateCom.Connection = Connection;
        updateCom.CommandText = updateText;
        SqliteDataReader reader = updateCom.ExecuteReader();
        Connection.Close();
    }
}
