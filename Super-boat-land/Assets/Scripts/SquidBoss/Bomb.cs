using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool hooked;
    public float hookSpeed;
    private GameObject player;
    public GameObject explosionEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hooked)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, hookSpeed * Time.deltaTime);
        }
    }

    public void Hook()
    {
        hooked = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UnHook()
    {
        hooked = false;
    }

    public void Explode()
    {
        Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
