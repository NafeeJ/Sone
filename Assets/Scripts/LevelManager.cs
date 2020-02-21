using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float timeTilRespawn;
    public int coinAmount;
    public Text coinText;
    public Image cherryHealth;
    public Sprite cherryComplete;
    public Sprite cherryHalf;
    public Sprite cherryEmpty;
    public int maxHealth;
    public int currentHealth;
    public bool invincibilityFrames;
    public int startLivesCount;
    public int currentLivesCount;
    public Text livesText;
    public GameObject gameOverScreen;
    public int bonusLifeLimit;
    public AudioSource coinSound;
    public AudioSource gameOverSound;
    public AudioSource levelMusic;
    public AudioSource endLevelMusic;
    public AudioSource victoryEndLevel;
    public bool respawnActive;

    private int coinExtraLifeCounter;
    private PlayerController thePlayer;
    private bool canRespawn;
    private ResetOnPlayerDeath[] objectsResetting;

    void Start()
    {
        if (PlayerPrefs.HasKey("CoinAmount"))
        {
            coinAmount = PlayerPrefs.GetInt("CoinAmount");
        }

        if (PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLivesCount = PlayerPrefs.GetInt("PlayerLives");
        }
        else
        {
            currentLivesCount = startLivesCount;
        }

        thePlayer = FindObjectOfType<PlayerController>();
        coinText.text = "x " + coinAmount;
        currentHealth = maxHealth;
        objectsResetting = FindObjectsOfType<ResetOnPlayerDeath>();
        livesText.text = "x " + currentLivesCount;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Respawn();
        }

        if (coinExtraLifeCounter >= bonusLifeLimit)
        {
            currentLivesCount++;
            livesText.text = "x " + currentLivesCount;
            coinExtraLifeCounter -= bonusLifeLimit;
        }
    }

    public void Respawn()
    {
        if (!canRespawn)
        {
            currentLivesCount -= 1;
            livesText.text = "x " + currentLivesCount;

            if (currentLivesCount > 0)
            {
                canRespawn = true;
                StartCoroutine("RespawnCoroutine");
            }
            else
            {
                thePlayer.gameObject.SetActive(false);
                livesText.text = "x " + 0;
                gameOverScreen.SetActive(true);
                levelMusic.Stop();
                gameOverSound.Play();
            }
        }

    }

    public IEnumerator RespawnCoroutine()
    {
        respawnActive = true;

        thePlayer.gameObject.SetActive(false);

        yield return new WaitForSeconds(timeTilRespawn);

        currentHealth = maxHealth;
        canRespawn = false;
        UpdateCherryHealth();
        thePlayer.transform.position = thePlayer.respawnPosition;

        foreach (ResetOnPlayerDeath obj in objectsResetting)
        {
            obj.gameObject.SetActive(true);
            obj.ResetAfterDeath();
        }

        coinAmount = 0;
        coinExtraLifeCounter = 0;
        coinText.text = "x " + 0;
        thePlayer.gameObject.SetActive(true);

        //Resetting Player
        thePlayer.myRB.velocity = Vector3.zero;
        thePlayer.theSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        respawnActive = false;
    }

    public void AddCoins(int amountOfCoins)
    {
        coinAmount += amountOfCoins;
        coinExtraLifeCounter += amountOfCoins;
        coinText.text = "x " + coinAmount;
        coinSound.Play();
    }


    public void DamagePlayer(int damageAmount, bool alwaysKill)
    {
        if (!invincibilityFrames || alwaysKill)
        {
            currentHealth -= damageAmount;
            UpdateCherryHealth();
            if (currentHealth >= 50)
            {
                thePlayer.Knockback();
            }
            thePlayer.hurtSound.Play();
        }
    }

    public void UpdateCherryHealth()
    {
        if (currentHealth > 50)
        {
            cherryHealth.sprite = cherryComplete;
        }
        else if (currentHealth > 0 && currentHealth <= 50)
        {
            cherryHealth.sprite = cherryHalf;
        }
        else if (currentHealth <= 0)
        {
            cherryHealth.sprite = cherryEmpty;
        }
    }

    public void AddLife(int livesToAdd)
    {
        currentLivesCount += livesToAdd;
        livesText.text = "x " + currentLivesCount;
        coinSound.Play();
    }

    public void GiveHealth(int healthToGive)
    {
        currentHealth += healthToGive;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        coinSound.Play();

        UpdateCherryHealth();
    }
}
