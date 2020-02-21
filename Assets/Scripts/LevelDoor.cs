using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    public string levelToLoad;
    public bool isUnlocked;
    public Sprite doorOpened;
    public Sprite doorClosed;

    private SpriteRenderer theSpriteRenderer;

    void Start()
    {
        PlayerPrefs.SetInt("Level 1", 1);
        theSpriteRenderer = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.GetInt(levelToLoad) == 1)
        {
            isUnlocked = true;
        }
        else
        {
            isUnlocked = false;
        }

        if (isUnlocked)
        {
            theSpriteRenderer.sprite = doorOpened;
        }
        else
        {
            theSpriteRenderer.sprite = doorClosed;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Jump") && isUnlocked)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}
