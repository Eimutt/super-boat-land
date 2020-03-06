using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crew : MonoBehaviour
{
	public Vector2 Position;
    public Vector2 Velocity;
    public Vector2 Acceleration;
	public CrewManager CrewManager { get; set; }
	public Settings Settings { get; set; }
	
    // Start is called before the first frame update
    void Start()
    {
		Position = transform.position;
		Debug.Log(Position);
		Velocity = Vector2.zero;
		Acceleration = Vector2.zero;
        
    }

    // Update is called once per frame
    public void UpdateCrew(float deltaTime)
    {
		Acceleration = Vector2.zero;
		Acceleration += getAttackForce();
		
		Velocity += deltaTime * Acceleration;
		// drag
		Velocity = Velocity * 0.98f;
        Position +=  0.5f * deltaTime * deltaTime * Acceleration + deltaTime * Velocity;
		transform.position = Position;
        
    }
	Vector2 getAttackForce(){
		Vector2 force = Vector2.zero;
		
		foreach (Enemy enemy in Settings.EnemyManager.GetEnemies())
        {
			//Debug.Log(enemy);
			
			float distance = (enemy.Position - Position).magnitude;
			if (distance < Settings.AttackRange){
				force = enemy.Position - Position;
				
			}
			
		}
		
		return force*2;
	}
	
}
