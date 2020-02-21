using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public string levelSelect;
    public string mainMenu;
    public GameObject thePauseScreen;

    private LevelManager theLevelManager;
    private PlayerController thePlayer;

    //THIRD HERE
    //want to be able to select menu options without having the use
    //the mouse
    public GameObject pauseScreenButton;

    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SECOND HERE
            //our TimeScale is telling us if it is
            //paused or not when it is 0 game is paused
            //any other number game is not paused
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    //START HERE
    //want a method that handles pausing the game
    //this method will have our pausing code
    public void PauseGame()
    {
        //FIFTH HERE
        //get the current event system there is only ever one
        //and make the current object be our pause button
        EventSystem.current.SetSelectedGameObject(pauseScreenButton);

        Time.timeScale = 0;
        thePauseScreen.SetActive(true);
        thePlayer.canMove = false;
        theLevelManager.levelMusic.Pause();
    }

    public void ResumeGame()
    {
        thePauseScreen.SetActive(false);
        Time.timeScale = 1f;
        thePlayer.canMove = true;
        theLevelManager.levelMusic.UnPause();
    }

    public void LevelSelectLoad()
    {
        PlayerPrefs.SetInt("CoinAmount", theLevelManager.coinAmount);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLivesCount);
        Time.timeScale = 1f;

        SceneManager.LoadScene(levelSelect);
    }

    public void MainMenuLoad()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
