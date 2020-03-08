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
	private int health = 1000;
	
    // Start is called before the first frame update
    void Start()
    {
		//Captain = GetComponent<LandMovementHandler>();
		//Captain = GameObject.FindObjectsOfType<LandMovementHandler>()[0];
		Position = transform.position;
		//Debug.Log(Position);
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
	Enemy target;
	//public List<Enemy> enemiesInRange = new List<Enemy>();
	//public List<Enemy> enemiesKilled = new List<Enemy>();
	
	Vector2 getAttackForce(){
		Vector2 force = Vector2.zero;
		//float targetRange = Settings.AttackRange;
		float distanceToEnemy = Settings.AttackRange+0.1f;
		
		/*
		foreach (Enemy enemy in Settings.EnemyManager.GetEnemies())
		{

			float distance = (enemy.Position - Position).magnitude;
			if (distance < Settings.AttackRange){
			enemiesInRange.Add(enemy);	
			} else {enemiesInRange.Remove(enemy);}
			
		}
		*/
		foreach (Enemy enemy in Captain.getEnemiesInRange())
		{
			//Debug.Log(enemy);
			//bool targetIsAlive = enemy.getIsAlive();
			//Debug.Log(Captain.getEnemiesInRange());
			if ((enemy.transform.position - Captain.transform.position).magnitude <= distanceToEnemy && !enemy.Equals(null)){
				distanceToEnemy = (enemy.transform.position - transform.position).magnitude;
				if (attacking){
					force = enemy.Position - Position;
				} else {
					force = Captain.getPosition() - Position;
				}
			}
			//Debug.Log(distanceToEnemy);
			//Debug.Log(Settings.HurtRange);
			if (distanceToEnemy<Settings.HurtRange && !Captain.enemiesKilled.Contains(enemy.gameObject) && !enemy.Equals(null)){
				
				//Debug.Log("trying to attack");
				if(enemy.damage(10)){
					Destroy(enemy.gameObject);
					//enemiesKilled.Add(enemy);
					//enemiesInRange.Remove(enemy);
				}
			}
			distanceToEnemy = Settings.AttackRange+0.1f;
			
		}
		
		if (Captain.enemiesInRange.Count == 0 || !attacking){
			force = Captain.getPosition() - Position;
		}
		
		//Debug.Log(Captain.enemiesInRange.Count);
		
		return force*10;
	}
	
}
