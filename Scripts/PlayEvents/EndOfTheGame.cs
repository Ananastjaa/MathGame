using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EndOfTheGame : MonoBehaviour
{
    public static Action<int> RecordUdated; 

    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _endTekst;

    public IEnumerator EndGame(string text)
    {
        Coins.SaveCoinCount();
        yield return new WaitForSeconds(1f);

        Time.timeScale = 0f;
        if (ScoreManager.IsRecordUpdated) RecordUdated?.Invoke(Level_change.level);
        ScoreManager.Score = 0;
        _gameOverPanel.SetActive(true);
        _endTekst.text = text;
    }
}