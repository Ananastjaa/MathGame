using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    private GameObject _sprite;

    private void Start()
    {
        var spriteIndex = PlayerPrefs.GetInt("skin_index");

        if (spriteIndex != 0)
        {
            var prevSprite = transform.GetChild(0).gameObject;
            _sprite = transform.GetChild(spriteIndex).gameObject;
            prevSprite.SetActive(false);
            _sprite.SetActive(true);
        }
    }
}
