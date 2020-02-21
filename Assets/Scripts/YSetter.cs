using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSetter : MonoBehaviour
{
    private CameraController theCamera;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theCamera.lowestPointInLevel = gameObject.transform.position.y;
        }
    }
}
