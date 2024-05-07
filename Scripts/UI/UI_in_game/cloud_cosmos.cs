using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud_cosmos : MonoBehaviour
{
    public float scaleChange;
    public Sprite sprite1;
    public Sprite sprite2;
    private static SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (ScoreManager.Score >= Level_change.secondChange) ChangeSprite();
        else spriteRenderer.sprite = sprite1;
    }

    private void OnEnable()
    {
        ScoreManager.Change_2 += ChangeSprite;
    }

    private void OnDisable()
    {
        ScoreManager.Change_2 -= ChangeSprite;
    }

    private void ChangeSprite()
    {
        float fewSeconds = 0;
        while (fewSeconds < 1) fewSeconds += Time.deltaTime;

        spriteRenderer.transform.localScale = new Vector3(scaleChange, scaleChange + 0.05f, scaleChange);
        spriteRenderer.sprite = sprite2;
    }
}
