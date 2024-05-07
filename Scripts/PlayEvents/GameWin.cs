using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameWin : MonoBehaviour
{
    private static Collider2D _deleteZoneCollider;
    private static Platform _deleteZonePlatformScript;

    private void Start()
    {
        _deleteZoneCollider = GetComponent<Collider2D>();
        _deleteZonePlatformScript = GetComponent<Platform>();
    }

    public static IEnumerator Win(GameObject WinPanel, Text EndText, Text EndWinScore)
    {
        Timer.Delta = 0;
        _deleteZoneCollider.isTrigger = false;
        _deleteZonePlatformScript.enabled = true;
        Coins.SaveCoinCount();

        yield return new WaitForSeconds(4f);

        WinPanel.SetActive(true);
        Time.timeScale = 0f;
        ScoreManager.Score = 0;

        if (Level_change.level == 1)
        {
            EndText.text = "Tu aprēķināji " + Level_change.endGame / 50 + " piemērus pa " + (1 - Timer.Min).ToString() + " min " + (60 - Timer.Seconds).ToString() + " sec";
        }
        else
        {
            EndText.text = "Tu aprēķināji " + Level_change.endGame / 50 + " piemērus pa " + Timer.Min.ToString() + " min " + Timer.Seconds.ToString() + " sec";
        }
        EndWinScore.text = "Rezultāts: " + Level_change.endGame;
    }
}