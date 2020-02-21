using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private LevelManager theLevelManager;
    public int coinValue;
    private bool pickable;
    private float timeTilPickable;

    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
        pickable = false;
        StartCoroutine(CoinActivate());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && pickable)
        {
            theLevelManager.AddCoins(coinValue);
            gameObject.SetActive(false);
        }
    }

    public IEnumerator CoinActivate()
    {
        yield return new WaitForSeconds(0.01f);
        pickable = true;
    }

    private void OnDisable()
    {
        theLevelManager.coinSound.Play();
    }
}
