using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Vector2 Position;
	public EnemyManager EnemyManager { get; set; }
    public Settings Settings { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void UpdateEnemy(float deltaTime){
		Position = transform.position;
	}
}
