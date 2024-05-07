using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class text_change : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        if (ScoreManager.Score >= Level_change.secondChange) Change(); 
    }

    private void OnEnable()
    {
        ScoreManager.Change_2 += ChangeAfterFewSeconds;
    }

    private void OnDisable()
    {
        ScoreManager.Change_2 -= ChangeAfterFewSeconds;
    }

    private void Change()
    {
        StartCoroutine(ChangeTextColour(0));
    }

    private void ChangeAfterFewSeconds()
    {
        StartCoroutine(ChangeTextColour(1));
    }

    private IEnumerator ChangeTextColour(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _text.color = Color.white;
    }
}
