using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public string levelSelect;
    public string mainMenu;

    private LevelManager theLevelManager;

    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    public void RestartLevel()
    {
        PlayerPrefs.SetInt("CoinAmount", 0);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.startLivesCount);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelectLoad()
    {
        PlayerPrefs.SetInt("CoinAmount", 0);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.startLivesCount);

        SceneManager.LoadScene(levelSelect);
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
