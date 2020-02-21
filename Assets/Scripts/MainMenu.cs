using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public string levelSelect;

    //START HERE
    //want to find the name of all
    //the levels in the game that get locked when we
    //start a new game
    //we will fill this in by hand this time
    //array size will be 2 and we type in the two level names we currently have
    public string[] levelNames;

    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);

        //SECOND HERE
        //whenever the player starts a new game we want
        //to go through our array and set all the 
        //level ints to be 0 so all levels
        //are locked
        for (int i = 0; i < levelNames.Length; i++)
        {
            //get the specific level name from the index
            //and set it int value to 0
            PlayerPrefs.SetInt(levelNames[i], 0);
        }

        //THIRD HERE
        //we want to update our player prefs to be what they
        //are supposed to be when we start the game
        PlayerPrefs.SetInt("CoinAmount", 0);
        PlayerPrefs.SetInt("PlayerLives", 3);
    }

    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
