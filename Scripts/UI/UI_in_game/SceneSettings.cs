using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSettings : MonoBehaviour
{
    public GameObject PausePanel;

    public void PauseButtonPressed()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueButtonPressed()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MenuScene(int scene)
    {
        Coins.SaveCoinCount();
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void RestartButtonPressed()
    {
        Coins.SaveCoinCount();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}

