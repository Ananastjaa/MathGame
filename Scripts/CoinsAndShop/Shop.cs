using System;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Action CoinssWasSepnt; 

    public int ChosenSkinIndex;
    public Skins[] SkinArray;

    [SerializeField] private Button _byButton;
    [SerializeField] private Text _buttonText;

    private static int _index;
    private Skins _shownSkin;
    private string _chosenSkinIndexPref = "skin_index";
    private static bool _isSkinPrefsUpdated = false;

    private void Start()
    {
        if (!_isSkinPrefsUpdated)
        {
            _isSkinPrefsUpdated = true;
            SetSkinStates();
        }

        _index = PlayerPrefs.GetInt(_chosenSkinIndexPref);
        SkinArray[_index].gameObject.SetActive(true);
        _shownSkin = SkinArray[_index];
        ChosenSkinIndex = _index;

        if (ChosenSkinIndex == _index)
        {
            _buttonText.text = "Izvēlēts";
            _byButton.interactable = false;
        }
        else if (_shownSkin.IsBought)
        {
            _buttonText.text = "Izvēlēties";
            _byButton.interactable = true;
        }
        else
        {
            _buttonText.text = _shownSkin.Cost.ToString();
            _byButton.interactable = true;
        }
    }

    public void GoNext()
    {
        if(_index < SkinArray.Length - 1)
        {
            _index++;
            ChangeProduct(_index);
        }
    }

    public void GoPrevious()
    {
        if (_index > 0)
        {
            _index--;
            ChangeProduct(_index);
        }
    }

    private void ChangeProduct(int index)
    {
        _shownSkin.gameObject.SetActive(false);
        SkinArray[index].gameObject.SetActive(true);
        _shownSkin = SkinArray[index];
        _shownSkin.Start();

        if (ChosenSkinIndex == index)
        {
            _buttonText.text = "Izvēlēts";
            _byButton.interactable = false;
        }
        else if (_shownSkin.IsBought)
        {
            _buttonText.text = "Izvēlēties";
            _byButton.interactable = true;
        }
        else
        {
            _buttonText.text = _shownSkin.Cost.ToString();
            _byButton.interactable = true;
        }
    }

    public void ByOrOn()
    {
        if(!_shownSkin.IsBought)
        {
            if (Coins.CoinCount >= _shownSkin.Cost)
            {
                DB.UpdateSkinRecord(_index);
                _shownSkin.SetBoughtFlag(true);
                ChangeChosenSkin();
                Coins.CoinCount -= _shownSkin.Cost;
                Coins.SaveCoinCount();
                CoinssWasSepnt?.Invoke();
            }
        }
        else ChangeChosenSkin();
    }

    private void ChangeChosenSkin()
    {
        ChosenSkinIndex = _index;
        _buttonText.text = "Izvēlēts";
        _byButton.interactable = false;
        PlayerPrefs.SetInt(_chosenSkinIndexPref, ChosenSkinIndex);
    }

    private void SetSkinStates()
    {
        PlayerPrefs.SetInt(_chosenSkinIndexPref, 0);
        var skinStates = DB.GetSkinStates();
        SkinArray[0].Start();
        SkinArray[0].SetBoughtFlag(true);

        for (int i = 0; i < SkinArray.Length - 1; i++)
        {
            SkinArray[i + 1].Start();
            if (skinStates[i] == 1) SkinArray[i + 1].SetBoughtFlag(true);
            else SkinArray[i + 1].SetBoughtFlag(false);
        }
    }
}
