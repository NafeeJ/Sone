using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1Battle : MonoBehaviour
{
    private CameraController theCamera;
    private LevelManager theLevelManager;
    private M1Controller m1;

    private bool battleActive;
    public float timeBetweenPockets;
    public float pocketTimer;
    public GameObject[] groundPockets;
    public float xAdjust;
    public float yAdjust;
    public GameObject bounds;

    // Start is called before the first frame update
    void Start()
    {
        battleActive = false;
        theCamera = FindObjectOfType<CameraController>();
        pocketTimer = timeBetweenPockets;
        m1 = FindObjectOfType<M1Controller>();
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theLevelManager.respawnActive)
        {
            battleActive = false;
            bounds.SetActive(false);
            theCamera.lowestPointInLevel = 0;
        }

        if (battleActive && !m1.dead)
        {
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(xAdjust, yAdjust, theCamera.transform.position.z), theCamera.easeIn * Time.deltaTime);

            if (pocketTimer > 0)
            {
                pocketTimer -= Time.deltaTime;
            }
            else
            {

                StartCoroutine(CreatePocket());
                StartCoroutine(CreatePocket());
                StartCoroutine(CreatePocket());
                StartCoroutine(CreatePocket());
                pocketTimer = timeBetweenPockets;
            }
        }
        else if (m1.dead)
        {
            bounds.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            battleActive = true;
            bounds.SetActive(true);
        }
    }

    public IEnumerator CreatePocket()
    {
        int pocketIndex = Random.Range(0, groundPockets.Length);
        groundPockets[pocketIndex].SetActive(false);
        yield return new WaitForSeconds(timeBetweenPockets);
        groundPockets[pocketIndex].SetActive(true);
    }
}
