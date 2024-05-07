
using UnityEngine;

public class FirstComplication : MonoBehaviour
{
    public static void CheckAndComplicate(GameObject platform)
    {
        if (ScoreManager.Score >= Level_change.firstComplication && ScoreManager.Score != Level_change.endGame)
        {
            platform.GetComponent<Animator>().SetTrigger("disapper");
        }
    }
}