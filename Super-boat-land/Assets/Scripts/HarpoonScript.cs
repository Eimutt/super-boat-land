using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonScript : MonoBehaviour
{
    // lifetime in  seconds
    private float maxLifeTime = 0.5f;
    private float currentLifeTime = 0f;
    public GameObject WaterPrefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= maxLifeTime)
        {
            GameObject waterParticles = Instantiate(WaterPrefab, transform);
            waterParticles.transform.parent = transform.parent;
            Destroy(gameObject);
        }
    }
}
