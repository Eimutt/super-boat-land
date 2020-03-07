using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Vector2 startPosition; //This should never change
    public Vector2 Position; //This should never change
    public EnemyManager EnemyManager { get; set; }
    public Settings Settings { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void UpdateEnemy(float deltaTime){
		Position = transform.position;
	}

    /*
     *  This is used for the AI to generate a random position.
     */
    public Vector2 getStartPosition()
    {
        return startPosition;
    }

    public float getHealth()
    {
        return 7.0f;
    }
}
