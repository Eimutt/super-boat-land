using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTrigger : MonoBehaviour
{
    public GameObject bossObject;
    private Vector2 playerBoatPos;
    private bool active;
    public float spawnOffSet;
    private Vector3 targetPos;
    public float moveSpeed;
    public GameObject tentaclePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (transform.position == targetPos)
            {
                bossObject.transform.position = transform.position;
                bossObject.SetActive(true);
                print("start boss fightt");
                Destroy(gameObject);
            }
        }
    }

    public void StartBossFight()
    {
        playerBoatPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        targetPos = playerBoatPos;
        targetPos.x += spawnOffSet;
        active = true;
        createArena();
    }

    void createArena()
    {
        float angle = 0;
        Vector2 spawnPos;
        for(int i = 0; i < 36; i++)
        {
            angle = i * 10 * Mathf.Deg2Rad;
            spawnPos = playerBoatPos + new Vector2(Mathf.Cos(angle) * spawnOffSet, Mathf.Sin(angle) * spawnOffSet / 2);
            var tentacle = Instantiate(tentaclePrefab, spawnPos, Quaternion.identity);
            tentacle.SetActive(true);
        }
    }
}
