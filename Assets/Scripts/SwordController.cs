using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject enemyDeath;
    private GameObject deathTemp;
    public GameObject coin;
    public M1Controller m1;

    void Start()
    {
        m1 = FindObjectOfType<M1Controller>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            deathTemp = Instantiate(enemyDeath, other.gameObject.transform.position, other.gameObject.transform.rotation);
            for (int i = 0; i < 5; i++)
            {
                Instantiate(coin, other.gameObject.transform.position, other.gameObject.transform.rotation);
            }
            Destroy(deathTemp, 0.25f);
        }

        if (other.tag == "Maanav")
        {
            m1.Die();
        }
    }
}
