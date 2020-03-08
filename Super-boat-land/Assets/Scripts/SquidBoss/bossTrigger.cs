using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTrigger : MonoBehaviour
{
    public GameObject bossObject;
    private GameObject playerBoat;
    private bool active;
    public float spawnOffSet;
    private Vector3 targetPos;
    public float moveSpeed;
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
        playerBoat = GameObject.FindGameObjectWithTag("Player");
        targetPos = playerBoat.transform.position;
        targetPos.x += spawnOffSet;
        active = true;
    }
}
