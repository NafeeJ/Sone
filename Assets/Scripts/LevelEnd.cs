using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string levelToLoad;
    public float whenToLoad;
    public string levelToUnlock;

    private PlayerController thePlayer;
    private CameraController theCamera;
    private LevelManager theLevelManager;
    private bool alreadyPlayed;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("LevelEndCoroutine");
        }
    }

    public IEnumerator LevelEndCoroutine()
    {
        thePlayer.canMove = false;
        theCamera.following = false;
        theLevelManager.invincibilityFrames = true;
        thePlayer.myRB.velocity = Vector3.zero;

        thePlayer.myAnimator.SetBool("kneeling", true);

        //theLevelManager.levelMusic.Stop();

        //if (!alreadyPlayed)
        //{
        //    theLevelManager.victoryEndLevel.Play();
        //    theLevelManager.endLevelMusic.PlayDelayed(1.5f);
        //    alreadyPlayed = true;
        //}

        PlayerPrefs.SetInt("CoinAmount", theLevelManager.coinAmount);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLivesCount);
        PlayerPrefs.SetInt(levelToUnlock, 1);

        //theLevelManager.endLevelMusic.Play();

        yield return new WaitForSeconds(whenToLoad);

        SceneManager.LoadScene(levelToLoad);
    }
}
