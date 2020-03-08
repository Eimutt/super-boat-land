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

    public GameObject Tentacle;
    public int tentacleCount;
    public float tentacleDelay;
    private float tentacleTimer;
    public float randomSpawnRange;

    public int collisionDamage;

    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Player");
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
            switch (rand)
            {
            case 0:
                TentacleAttack();
                break;
            case 1:
                Suck();
                break;
            case 2:
                Blow();
                break;
            case 3:
                DoNothing();
                break;
            }
            isActive = true;
        }
        tentacleTimer += Time.deltaTime * Random.Range(0.5f, 1.5f);
        if(tentacleTimer > tentacleDelay)
        {
            SpawnTentacle();
            tentacleTimer = 0;
        }

    }

    void Suck()
    {
        suck.SetActive(true);
    }

    void Blow()
    {
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

    void TentacleAttack()
    {
        print("tentacles");
        for(int i = 0; i < tentacleCount; i++)
        {
            SpawnTentacle();
        }
        
    }

    void SpawnTentacle()
    {
        Vector3 spawnPos = boat.transform.position;
        var tentacle = Instantiate(Tentacle, spawnPos, Quaternion.identity);
        tentacle.SetActive(true);

        //Random number of extra random tentacles
        int randomNumber = Random.Range(0, 3);
        for(int i = 0; i <= randomNumber; i++)
        {
            spawnPos = boat.transform.position;
            float randx = Random.Range(-randomSpawnRange, randomSpawnRange);
            float randy = Random.Range(-randomSpawnRange, randomSpawnRange);
            spawnPos.x += randx;
            spawnPos.y += randy;
            tentacle = Instantiate(Tentacle, spawnPos, Quaternion.identity);
            tentacle.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Boat>().TakeDamage(collisionDamage);
        } else if (other.tag == "Bomb")
        {
            other.GetComponent<Bomb>().Explode();
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        health -= 1;
        if(health <= 0)
        {
            DestroyTentacles();
            Destroy(gameObject);

        }
    }

    void DestroyTentacles()
    {
        GameObject[] tentacles = GameObject.FindGameObjectsWithTag("Tentacle");
        foreach(GameObject tentacle in tentacles)
        {
            Destroy(tentacle);
        }
    }
}
