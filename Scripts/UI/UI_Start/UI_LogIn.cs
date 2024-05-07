using UnityEngine;

public class UI_LogIn : MonoBehaviour
{
    [SerializeField] private GameObject _choicePanel, _logInPanel, _signInPanel;

    public void BackFromLogInButtonPressed()
    {
        _logInPanel.SetActive(false);
        _choicePanel.SetActive(true);
    }

    public void BackFromSignInButtonPressed()
    {
        _signInPanel.SetActive(false);
        _choicePanel.SetActive(true);
    }

    public void GoToLogInButtonPressed()
    {
        _logInPanel.SetActive(true);
        _choicePanel.SetActive(false);
    }

    public void GoToSignInButtonPressed()
    {
        _signInPanel.SetActive(true);
        _choicePanel.SetActive(false);
    }
}
