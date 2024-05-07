using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static Action<int, string, int> ScoreDataUpdated;

    public static Action Change_1;
    public static Action Change_2;
    public static Action Change_3;
    public static Action PlayerWin;
    public static bool IsRecordUpdated;

    public static int Score;
    public static int secondsFirstLev = 0;
    public static int minFirstLev = 0;
    public static int secondsSecLev = 0;
    public static int minSecLev = 0;
    public static int scoreFirstLev;
    public static int scoreSecLev;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text finishScoreText;
    public Text BestTimeText;

    public GameObject WinPanel;
    [SerializeField] private Text EndText;
    [SerializeField] private Text EndWinScore;
    [SerializeField] private GameWin _gameWinScript;

    private void OnEnable()
    {
        RightAnswer.ScoreUdated += UpdateScores;
        EndOfTheGame.RecordUdated += InvokeUpdateDB;
    }

    private void OnDisable()
    {
        RightAnswer.ScoreUdated -= UpdateScores;
        EndOfTheGame.RecordUdated -= InvokeUpdateDB;
    }

    private void Start()
    {
        Score = 0;
        IsRecordUpdated = false;

        UpdatePlayerPrefs();
        UpdateScores();
    }

    public void UpdateScores()
    {
        scoreText.text = Score.ToString();

        if (Level_change.level == 1) FirstLevUpdate();
        else if (Level_change.level == 2) SecLevUpdate();
        else ThirdLevUpdate();

        CheckChanges();
        CheckComplications();
    }

    private void CheckChanges()
    {
        if (Score == Level_change.firstChange) Change_1?.Invoke();
        
        else if (Score == Level_change.secondChange) Change_2?.Invoke();
        
        else if (Score == Level_change.thirdChange) Change_3?.Invoke();

        else if (Score == Level_change.endGame)
        {
            PlayerWin?.Invoke();
            InvokeUpdateDB(Level_change.level);
            StartCoroutine(GameWin.Win(WinPanel, EndText, EndWinScore));
        }   
    }

    private void CheckComplications()
    {
        if (Score == Level_change.secondComplication)
        {
            SecondComplication.Complicate();
        }
    }

    private void FirstLevUpdate()
    {
        if (PlayerPrefs.GetInt("scoreFirstLev") <= Score)
        {
            if (PlayerPrefs.GetInt("scoreFiertLev") < Score)
            {
                PlayerPrefs.SetInt("BestTimeFirstLev", Timer.Time);
                PlayerPrefs.SetInt("minFirstLev", 1 - Timer.Min);
                PlayerPrefs.SetInt("secondsFirstLev", 60 - Timer.Seconds);
                IsRecordUpdated = true;
            }
            else if (PlayerPrefs.GetInt("BestTimeFirstLev") > Timer.Time)
            {
                PlayerPrefs.SetInt("BestTimeFirstLev", Timer.Time);
                PlayerPrefs.SetInt("minFirstLev", 1 - Timer.Min);
                PlayerPrefs.SetInt("secondsFirstLev", 60 - Timer.Seconds);
                IsRecordUpdated = true;
            }
            PlayerPrefs.SetInt("scoreFirstLev", Score);
        }

        bestScoreText.text = PlayerPrefs.GetInt("scoreFirstLev").ToString();
        BestTimeText.text = PlayerPrefs.GetInt("minFirstLev").ToString("D2") + ":" + PlayerPrefs.GetInt("secondsFirstLev").ToString("D2");
        finishScoreText.text = Score.ToString();
    }

    private void SecLevUpdate()
    {
        string bestScore = PlayerPrefs.GetInt("scoreSecLev").ToString();
        string bestTime = PlayerPrefs.GetInt("minSecLev").ToString("D2") + ":" + PlayerPrefs.GetInt("secondsSecLev").ToString("D2");

        if (PlayerPrefs.GetInt("scoreSecLev") <= Score)
        {
            if (PlayerPrefs.GetInt("scoreSecLev") < Score)
            {
                PlayerPrefs.SetInt("BestTimeSecLev", Timer.Time);
                PlayerPrefs.SetInt("minSecLev", Timer.Min);
                PlayerPrefs.SetInt("secondsSecLev", Timer.Seconds);
                IsRecordUpdated = true;
            }
            else if (PlayerPrefs.GetInt("BestTimeSecLev") > Timer.Time)
            {
                PlayerPrefs.SetInt("BestTimeSecLev", Timer.Time);
                PlayerPrefs.SetInt("minSecLev", Timer.Min);
                PlayerPrefs.SetInt("secondsSecLev", Timer.Seconds);
                IsRecordUpdated = true;
            }       
            PlayerPrefs.SetInt("scoreSecLev", Score);
        }

        bestScoreText.text = PlayerPrefs.GetInt("scoreSecLev").ToString();
        finishScoreText.text = Score.ToString();
        BestTimeText.text = PlayerPrefs.GetInt("minSecLev").ToString("D2") + ":" + PlayerPrefs.GetInt("secondsSecLev").ToString("D2");
    }

    private void ThirdLevUpdate()
    {
        if (PlayerPrefs.GetInt("ScoreManager") <= Score)
        {
            if (PlayerPrefs.GetInt("ScoreManager") < Score)
            {
                PlayerPrefs.SetInt("BestTime", Timer.Time);
                PlayerPrefs.SetInt("Min", Timer.Min);
                PlayerPrefs.SetInt("Seconds", Timer.Seconds);
                IsRecordUpdated = true;
            }
            else if (PlayerPrefs.GetInt("BestTime") > Timer.Time)
            {
                PlayerPrefs.SetInt("BestTime", Timer.Time);
                PlayerPrefs.SetInt("Min", Timer.Min);
                PlayerPrefs.SetInt("Seconds", Timer.Seconds);
                IsRecordUpdated = true;
            }
            PlayerPrefs.SetInt("ScoreManager", Score);
        }

        bestScoreText.text = PlayerPrefs.GetInt("ScoreManager").ToString();
        finishScoreText.text = Score.ToString();
        BestTimeText.text = PlayerPrefs.GetInt("Min").ToString("D2") + ":" + PlayerPrefs.GetInt("Seconds").ToString("D2");
    }

    private void UpdatePlayerPrefs()  
    {
        var scores = DB.GetUserScores();
        if(scores["times"][0].ToString() == "000")
        {
            PlayerPrefs.SetInt("BestTimeFirstLev", 0);
            PlayerPrefs.SetInt("minFirstLev", 0);
            PlayerPrefs.SetInt("secondsFirstLev", 0);
        }
        else
        {
            var ArrTime1 = scores["times"][0].ToString().Split(':');
            var min1 = int.Parse(ArrTime1[0]);
            var sec1 = int.Parse(ArrTime1[1]);
            var time1 = min1 * 100 + sec1;
            PlayerPrefs.SetInt("BestTimeFirstLev", time1);
            PlayerPrefs.SetInt("minFirstLev", min1);
            PlayerPrefs.SetInt("secondsFirstLev", sec1);
        }

        if (scores["times"][1].ToString() == "000")
        {
            PlayerPrefs.SetInt("BestTimeSecLev", 0);
            PlayerPrefs.SetInt("minSecLev", 0);
            PlayerPrefs.SetInt("secondsSecLev", 0);
        }
        else 
        {
            var ArrTime2 = scores["times"][1].ToString().Split(':');
            var min2 = int.Parse(ArrTime2[0]);
            var sec2 = int.Parse(ArrTime2[1]);
            var time2 = min2 * 100 + sec2;
            PlayerPrefs.SetInt("BestTimeSecLev", time2);
            PlayerPrefs.SetInt("minSecLev", min2);
            PlayerPrefs.SetInt("secondsSecLev", sec2);
        }

        if (scores["times"][2].ToString() == "000")
        {
            PlayerPrefs.SetInt("BestTime", 0);
            PlayerPrefs.SetInt("Min", 0);
            PlayerPrefs.SetInt("Seconds", 0);
        }
        else
        {
            var ArrTime3 = scores["times"][2].ToString().Split(':');
            var min3 = int.Parse(ArrTime3[0]);
            var sec3 = int.Parse(ArrTime3[1]);
            var time3 = min3 * 100 + sec3;
            PlayerPrefs.SetInt("BestTime", time3);
            PlayerPrefs.SetInt("Min", min3);
            PlayerPrefs.SetInt("Seconds", sec3);
        }

        PlayerPrefs.SetInt("scoreFirstLev", int.Parse(scores["scores"][0].ToString()));
        PlayerPrefs.SetInt("ScoreManager", int.Parse(scores["scores"][2].ToString()));
        PlayerPrefs.SetInt("scoreSecLev", int.Parse(scores["scores"][1].ToString()));
    }

    public void InvokeUpdateDB(int level)
    {
        if (level == 1) ScoreDataUpdated?.Invoke(Level_change.level, BestTimeText.text, PlayerPrefs.GetInt("scoreFirstLev"));
        else if (level == 2) ScoreDataUpdated?.Invoke(Level_change.level, BestTimeText.text, PlayerPrefs.GetInt("scoreSecLev"));
        else ScoreDataUpdated?.Invoke(Level_change.level, BestTimeText.text, PlayerPrefs.GetInt("ScoreManager"));
    }
}