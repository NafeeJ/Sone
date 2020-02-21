using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float moveSpeed;
    public float distanceTilAggro;
    private Transform thePlayer;
    public bool freezeY;
    private float initialY;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>().transform;
        if (distanceTilAggro == 0) { distanceTilAggro = 4; }
        initialY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeY)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialY, gameObject.transform.position.z);
        }

        if (Vector3.Distance(gameObject.transform.position, thePlayer.position) < distanceTilAggro)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, thePlayer.position, moveSpeed * Time.deltaTime);

            if (gameObject.transform.position.x < thePlayer.position.x)
            {
                gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            if (gameObject.transform.position.x >= thePlayer.position.x)
            {
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
