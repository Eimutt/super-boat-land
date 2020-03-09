using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    public float force;
    public bool push;///push or pull
    public Vector3 sourceDif;
    public Vector3 extraForce;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        direction = other.transform.position - transform.position - sourceDif + extraForce;
        if (!push)
            direction *= -1;

        if (other.tag == "Player")
        {
            other.GetComponent<CustomPhysics>().AddForce(direction, force);
        } else if (other.tag == "Bomb")
        {
            print("Moving bomb");
            other.GetComponent<CustomPhysics>().AddForce(direction, force);
        }
    }
}
