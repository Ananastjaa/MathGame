using System.Collections;
using UnityEngine;

public class cloud_change : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ScoreManager.Change_2 += Animate;
    }

    private void OnDisable()
    {
        ScoreManager.Change_2 -= Animate;
    }

    private void Animate()
    {
        anim.SetTrigger("change");
    }
}