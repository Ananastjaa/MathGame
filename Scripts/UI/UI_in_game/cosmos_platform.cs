using System.Collections;
using UnityEngine;

public class cosmos_platform : MonoBehaviour
{
    public float scaleChange;
    public Sprite sprite1;
    public Sprite sprite2;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (ScoreManager.Score >= Level_change.secondChange) Change();
        else _spriteRenderer.sprite = sprite1;
    }

    private void OnEnable()
    {
        ScoreManager.Change_2 += ChangeSprite;
    }

    private void OnDisable()
    {
        ScoreManager.Change_2 -= ChangeSprite;
    }

    private void Change()
    {
        StartCoroutine(CallPlatformChange(0));
    }

    private void ChangeSprite()
    {
        StartCoroutine(CallPlatformChange(1));
    }

    private IEnumerator CallPlatformChange(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _spriteRenderer.transform.localScale = new Vector3(scaleChange, scaleChange + 0.05f, 0.25f);
        _spriteRenderer.sprite = sprite2;
        if (gameObject.tag == "help")
        {
            _spriteRenderer.color = Color.red;
        }
    }
}
