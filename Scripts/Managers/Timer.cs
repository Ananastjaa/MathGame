using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static int Delta = 1;

    [SerializeField] private Text timeText;
    [SerializeField] private Text EndTimeText;

    private EndOfTheGame _endScript;

    private static int _time;
    private static int _seconds;
    private static int _min;

    public static int Time { get { return _time; } }
    public static int Seconds { get { return _seconds; } }
    public static int Min { get { return _min; } }

    private void Start()
    {
        Delta = 1;
        if (Level_change.level == 1)
        {
            _min = 2;
            _seconds = 0;
            StartCoroutine(TimerForFirstLev());
        }
        else
        {
            _min = 0;
            _seconds = 0;
            StartCoroutine(TimeFolow());
        }
    }

    IEnumerator TimeFolow()
    {
        while (true)
        {
            timeText.text = _min.ToString("D2") + " : " + _seconds.ToString("D2");
            EndTimeText.text = _min.ToString("D2") + ":" + _seconds.ToString("D2");

            if (_seconds == 59)
            {
                _min++;
                _seconds = -1;
            }
            _seconds += Delta;
            _time = _min * 100 + _seconds;

            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator TimerForFirstLev()
    {
        while (true)
        {
            if (_seconds <= 0)
            {
                _min--;
                _seconds = 60;
            }
            _seconds -= Delta;
            _time = _min * 60 + _seconds;
            timeText.text = _min.ToString("D2") + " : " + _seconds.ToString("D2");
            int a = 60 - _seconds;
            int b = 1 - _min;

            if (a == 60)
            {
                a = 0;
                b++;
            }

            EndTimeText.text = b.ToString("D2") + ":" + a.ToString("D2");
            yield return new WaitForSeconds(1);

            if (_time == 1)
            {
                _endScript = Player.instance.gameObject.GetComponent<EndOfTheGame>();
                StartCoroutine(_endScript.EndGame("TU ZAUDĒJI \r\n Laiks ir beidzies!"));
            }
        }
    }
}