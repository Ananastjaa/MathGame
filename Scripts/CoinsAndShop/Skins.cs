using UnityEngine;

public class Skins : MonoBehaviour
{
    public int Cost;
    public bool IsBought { get { return _isBought; } }

    private bool _isBought;
    private string _byStr;

    public void Start()
    {
        _byStr = gameObject.name + "By";

        if (PlayerPrefs.GetInt(_byStr) == 1) _isBought = true;
        else _isBought = false;
    }

    public void SetBoughtFlag(bool state)
    {
        _isBought = state;
        if (state) PlayerPrefs.SetInt(_byStr, 1);
        else PlayerPrefs.SetInt(_byStr, 0);
    }
}
