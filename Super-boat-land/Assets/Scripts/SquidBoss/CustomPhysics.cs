﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics : MonoBehaviour
{
    public float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
    private CharacterController character;
    public bool player;
    void Start()
    {
        if (player)
        {
            character = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (impact.magnitude > 0.2F)
        {
            if (player)
            {
                character.Move(impact * Time.deltaTime);
            } else
            {
                gameObject.transform.Translate(impact * Time.deltaTime);
            }
        }
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    public void AddForce(Vector3 direction, float force)
    {
        direction.Normalize();
        impact += direction.normalized * force / mass;

    }
}
