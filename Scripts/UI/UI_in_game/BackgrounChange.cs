using UnityEngine;

public class BackgrounChange : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ScoreManager.Change_1 += FirstChange;
        ScoreManager.Change_2 += SecondChange;
        ScoreManager.Change_3 += ThirdChange;
        ScoreManager.PlayerWin += WinAnimation;
    }

    private void OnDisable()
    {
        ScoreManager.Change_1 -= FirstChange;
        ScoreManager.Change_2 -= SecondChange;
        ScoreManager.Change_3 -= ThirdChange;
        ScoreManager.PlayerWin -= WinAnimation;
    }

    private void FirstChange()
    {
        _anim.SetTrigger("FirstChange");
    }

    private void SecondChange() 
    {
        _anim.SetTrigger("SecondChange");
    }

    private void ThirdChange()
    {
        _anim.SetTrigger("ThirdChange");
    }

    private void WinAnimation()
    {
        float laiks = 0f;

        while (laiks < 1)
        {
            laiks += Time.deltaTime;
        }

        _anim.SetTrigger("win");
    }
}
