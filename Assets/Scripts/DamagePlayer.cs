using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private LevelManager theLevelManager;
    public int damageToGive;
    public bool alwaysKill;

    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theLevelManager.DamagePlayer(damageToGive, alwaysKill);
        }

    }
}
