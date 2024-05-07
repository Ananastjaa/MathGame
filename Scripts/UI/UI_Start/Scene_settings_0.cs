using System;
using UnityEngine;

public class Scene_settings_0 : MonoBehaviour
{
    public static Action GameStarted;
    public GameObject SettingPanel, InstructionPanel, LevelPanel, ShopPanel, EndChoicePanel;
    public GameObject Error;

    public void ShopButtonPressed()
    {
        ShopPanel.SetActive(true);
    }

    public void BackFromShopButtonPressed()
    {
        ShopPanel.SetActive(false);
    }

    public void SettingButtonPressed()
    {
        SettingPanel.SetActive(true);
    }

    public void FromSetToMenuButtonPressed()
    {
        if (Operations.Instance.AllBoxesOff())
        {
            Error.SetActive(true);
            return;
        }
        else Error.SetActive(false);
        SettingPanel.SetActive(false);
    }

    public void InstructionButtonPressed()
    {
        InstructionPanel.SetActive(true);
    }

    public void FromInstToMenuButtonPressed()
    {
        InstructionPanel.SetActive(false);
    }

    public void FromLevToMenuButtonPressed()
    {
        LevelPanel.SetActive(false);
    }

    public void StartButtonPressed()
    {
        GameStarted?.Invoke();
        LevelPanel.SetActive(true);
    }

    public void ExitButtonPressed()
    {
        EndChoicePanel.SetActive(true);
    }

    public void LogOut(bool flag)
    {
        if(flag)PlayerPrefs.SetInt(Log_in.UserIn, 0);
        else PlayerPrefs.SetInt(Log_in.UserIn, 1);
        Application.Quit();
    }

    public void BackFromEndChoise()
    {
        EndChoicePanel.SetActive(false);
    }
}
