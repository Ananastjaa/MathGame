using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public static int CoinCount;
    private static string _coinPrefs = "coinCount";
    [SerializeField] private Text _coinText;

    private void Start()
    {
        CoinCount = PlayerPrefs.GetInt(_coinPrefs);
        UpdateCoinText();
    }

    private void OnEnable()
    {
        RightAnswer.ScoreUdated += UpdateCoinText;
        Shop.CoinssWasSepnt += UpdateCoinText;
    }

    private void OnDisable()
    {
        RightAnswer.ScoreUdated -= UpdateCoinText;
        Shop.CoinssWasSepnt -= UpdateCoinText;
    }

    private void UpdateCoinText()
    {
        _coinText.text = CoinCount.ToString();
    }

    public static void SaveCoinCount()
    {
        PlayerPrefs.SetInt(_coinPrefs, CoinCount);
        DB.UpdateCoinRecord();
    }
}
