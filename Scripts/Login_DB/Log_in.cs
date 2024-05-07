using UnityEngine;
using UnityEngine.UI;


public class Log_in : MonoBehaviour
{
    public static string UserIn = "userIn";

    [SerializeField] private InputField _inputedUsername;
    [SerializeField] private InputField _inputedPassword;
    [SerializeField] private InputField _inputedName;
    [SerializeField] private InputField _inputedNewUsername;
    [SerializeField] private InputField _inputedNewPassword;
    [SerializeField] private InputField _inputedNewPassword2;
    [SerializeField] private Text _incorrectUsernameText;
    [SerializeField] private Text _incorrectPasswordText;
    [SerializeField] private Text _incorrectPassrord2Text;
    [SerializeField] private Text _wrongPasswordText;
    [SerializeField] private GameObject _startPanel;

    private void Start()
    {
        if(PlayerPrefs.GetInt(UserIn) == 1) _startPanel.SetActive(false);
        else _startPanel.SetActive(true);
    }

    private void Update()
    {
        if (!CheckLenght(_inputedNewUsername.text))
        {
            _incorrectUsernameText.text = "Pārāk īss";
            _incorrectUsernameText.gameObject.SetActive(true);
        }
        else if (!DB.IsUsernameFree(_inputedNewUsername.text))
        {
            _incorrectUsernameText.text = "Šis lietotājvārds jau ir aizņemts";
            _incorrectUsernameText.gameObject.SetActive(true);
        }
        else _incorrectUsernameText.gameObject.SetActive(false);

        if (!CheckLenght(_inputedNewPassword.text)) _incorrectPasswordText.gameObject.SetActive(true);
        else _incorrectPasswordText.gameObject.SetActive(false);

        if (_inputedNewPassword.text != _inputedNewPassword2.text) _incorrectPassrord2Text.gameObject.SetActive(true);
        else _incorrectPassrord2Text.gameObject.SetActive(false);
    }

    public void LogIn()
    {
        if (!string.IsNullOrEmpty(_inputedName.text) && CheckLenght(_inputedNewUsername.text)
            && DB.IsUsernameFree(_inputedNewUsername.text)
            && CheckLenght(_inputedNewUsername.text)
            && _inputedNewPassword.text == _inputedNewPassword2.text)
        {
            DB.InsertNewUser(_inputedName.text, _inputedNewUsername.text, _inputedNewPassword.text);
            PlayerPrefs.SetInt(UserIn, 1);
            DB.CurrentUser = _inputedNewUsername.text;
            Coins.CoinCount = DB.GetCoins();
            Coins.SaveCoinCount();
            _startPanel.SetActive(false);
        }
    }

    public void SignIn ()
    {
        if (!DB.IsPasswordCorrect(_inputedUsername.text, _inputedPassword.text)) _wrongPasswordText.gameObject.SetActive(true);
        else _wrongPasswordText.gameObject.SetActive(false);

        if (!string.IsNullOrEmpty(_inputedUsername.text) && DB.IsPasswordCorrect(_inputedUsername.text, _inputedPassword.text))
        {
            PlayerPrefs.SetInt(UserIn, 1);
            DB.CurrentUser = _inputedUsername.text;
            Coins.CoinCount = DB.GetCoins();
            Coins.SaveCoinCount();
            _startPanel.SetActive(false);
        }
    }

    private bool CheckLenght(string word)
    {
        return (word.Length > 5);
    }
}
