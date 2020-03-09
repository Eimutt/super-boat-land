using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleMovement : MonoBehaviour
{
    float speed;
    float turnPoint;
    float distanceTraveled;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
        turnPoint = 1.0f;
        distanceTraveled = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<CharacterController>().Move(new Vector2(speed * Time.deltaTime, 0.0f));
        distanceTraveled += speed * Time.deltaTime;
        if (Mathf.Abs(distanceTraveled) > turnPoint)
        {
            speed *= -1;
            distanceTraveled = 0;
        }
    }
}
