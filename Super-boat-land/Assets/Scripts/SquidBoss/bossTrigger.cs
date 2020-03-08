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
                var boss = Instantiate(bossObject);
                boss.transform.position = transform.position;
                boss.SetActive(true);
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
        float angleDeg = 0;
        float angleRad = 0;
        Vector2 spawnPos;
        for(int i = 0; i < 36; i++)
        {
            angleDeg = i * 10;
            angleRad = angleDeg * Mathf.Deg2Rad;
            spawnPos = playerBoatPos + new Vector2(Mathf.Cos(angleRad) * spawnOffSet, Mathf.Sin(angleRad) * spawnOffSet / 2);
            var tentacle = Instantiate(tentaclePrefab, spawnPos, Quaternion.identity);
            tentacle.SetActive(true);
            tentacle.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, angleDeg + 90);
        }
    }
}
