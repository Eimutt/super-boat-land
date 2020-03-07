using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonScript : MonoBehaviour
{
    // lifetime in  seconds
    private float maxLifeTime = 0.5f;
    private float currentLifeTime = 0f;
    public GameObject WaterPrefab;
    public GameObject RopePrefab;
    GameObject rope;
    GameObject playerBoat;

    // Start is called before the first frame update
    void Start()
    {
        rope = Instantiate(RopePrefab);
        playerBoat = GameObject.FindGameObjectWithTag("Player");
        rope.transform.parent = transform.parent;
        rope.GetComponent<LineRenderer>().SetPosition(0, playerBoat.transform.position);
        rope.GetComponent<LineRenderer>().SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        rope.GetComponent<LineRenderer>().SetPosition(0, playerBoat.transform.position);
        rope.GetComponent<LineRenderer>().SetPosition(1, transform.position);
        print(transform.position);

        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= maxLifeTime)
        {
            GameObject waterParticles = Instantiate(WaterPrefab, transform);
            waterParticles.transform.parent = transform.parent;
            waterParticles.transform.rotation = Quaternion.Euler(0, 0, 0);
            Destroy(rope);
            Destroy(gameObject);
        }
    }
}
