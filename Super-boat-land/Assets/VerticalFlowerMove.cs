using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalFlowerMove : MonoBehaviour
{
    float speed;
    float turnPoint;
    float distanceTraveled;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1.0f, 3.0f);
        turnPoint = 10.0f;
        distanceTraveled = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<CharacterController>().Move(new Vector2(0,speed * Time.deltaTime));
        distanceTraveled += speed * Time.deltaTime;
        if(Mathf.Abs(distanceTraveled) > turnPoint)
        {
            speed *= -1;
            distanceTraveled = 0;
        }
    }
}
