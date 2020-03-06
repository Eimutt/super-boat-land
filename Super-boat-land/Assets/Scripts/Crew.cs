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
	public bool attacking = false;
	public LandMovementHandler Captain { get; set; }
	
    // Start is called before the first frame update
    void Start()
    {
		//Captain = GetComponent<LandMovementHandler>();
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
		Velocity = Velocity * 0.90f;
        Position +=  0.5f * deltaTime * deltaTime * Acceleration + deltaTime * Velocity;
		transform.position = Position;
        
    }
	Vector2 getAttackForce(){
		Vector2 force = Vector2.zero;
		//if (attacking){
			foreach (Enemy enemy in Settings.EnemyManager.GetEnemies())
			{
				//Debug.Log(enemy);
				
				float distance = (enemy.Position - Position).magnitude;
				if (distance < Settings.AttackRange && attacking){
					force = enemy.Position - Position;
					
				} else {
					force = Captain.getPosition() - Position;
				}
				
			}
		//} 
		
		return force*10;
	}
	
}
