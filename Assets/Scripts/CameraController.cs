using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float followAhead;
    public float easeIn;
    public bool following;
    public float lowestPointInLevel;

    private Vector3 targetPosition;

    void Start()
    {
        following = true;
    }

    void Update()
    {
        if (following)
        {
            targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

            if (targetPosition.y < lowestPointInLevel)
            {
                //gameObject.transform.position = new Vector3(gameObject.transform.position.x, lowestPointInLevel, gameObject.transform.position.z);
                targetPosition = new Vector3(target.transform.position.x, lowestPointInLevel, transform.position.z);
            }

            if (target.transform.localScale.x > 0f)
            {
                targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
            }
            else
            {
                targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, easeIn * Time.deltaTime);
        }
    }
}
