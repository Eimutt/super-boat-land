using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBoss : MonoBehaviour
{
    private bool isActive;
    private GameObject boat;
    private Vector3 distance;
    public float aggroRamge;
    private GameObject blow;
    private GameObject suck;

    public float pauseTime;
    public float attackTime;
    private float timer;
    private enum State { Pause, Suck, Blow}
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        boat = GameObject.Find("boat");
        blow = gameObject.transform.Find("BlowWind").gameObject;
        suck = gameObject.transform.Find("SuckWind").gameObject;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if(timer > attackTime)
            {
                timer = 0;
                isActive = false;
                Pause();
            }
        } else
        {
            int rand = Random.Range(0, 3);
            print(rand);
            switch (rand)
            {
            case 0:
                DoNothing();
                break;
            case 1:
                Suck();
                break;
            case 2:
                Blow();
                break;
            }
            isActive = true;
        }
    }

    void Suck()
    {
        print("sucking");
        suck.SetActive(true);
    }

    void Blow()
    {
        print("blowing");
        blow.SetActive(true);
    }

    void DoNothing()
    {
        print("chilling");
    }

    void Pause()
    {
        blow.SetActive(false);
        suck.SetActive(false);
    }
}
