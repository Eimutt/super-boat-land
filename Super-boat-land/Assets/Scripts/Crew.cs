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
	public int health;
	
	
    // Start is called before the first frame update
    void Start()
    {
		//Captain = GetComponent<LandMovementHandler>();
		//Captain = GameObject.FindObjectsOfType<LandMovementHandler>()[0];
		Position = transform.position;
		//Debug.Log(Position);
		Velocity = Vector2.zero;
		Acceleration = Vector2.zero;
		health = 1000;
        
    }

    // Update is called once per frame
    public void UpdateCrew(float deltaTime)
    {
		Acceleration = Vector2.zero;
		Acceleration += getAttackForce();
		Acceleration += getSeparationForce();
		Velocity += deltaTime * Acceleration;
		// drag
		Velocity = Velocity * 0.90f;
        Position +=  0.5f * deltaTime * deltaTime * Acceleration + deltaTime * Velocity;
		transform.position = Position;
        
    }
	Enemy target;
	//public List<Enemy> enemiesInRange = new List<Enemy>();
	//public List<Enemy> enemiesKilled = new List<Enemy>();
	Vector2 getSeparationForce(){
		
		Vector2 separationForce = Vector2.zero;
		
		foreach (GameObject neighbor in Settings.allLandObjects)
        {
			float distance = (neighbor.transform.position - transform.position).magnitude;
			
			if (neighbor!= this && distance < Settings.CrewManager.SeparationRadius && distance >0.05f)
			{
				Vector2 xyPosition1 = new Vector2(transform.position.x,transform.position.y); 
				Vector2 xyPosition2 = new Vector2(neighbor.transform.position.x, neighbor.transform.position.y);
				separationForce += ((Settings.CrewManager.SeparationRadius - distance) / distance) * (xyPosition1-xyPosition2);
			}
		}
		return separationForce*150;
	}
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
			
			force*=5;
		} else {
			force *= 10;
		}
		
		//Debug.Log(Captain.enemiesInRange.Count);
		
		return force;
	}
	// Call this function to damage crew. If the crew dies this returns true and the attacker should call Destroy(crewobject.gameObject)
	public bool damage(int damage)
    {
        setHealth(getHealth() - damage);
        if (getHealth() <= 0)
        {
            return true;
        }
        return false;
    }
	public int getHealth()
    {
        return health;
    }
	private void setHealth(int newHealth)
    {
        health = newHealth;
        //healthBar.SetHealth(health / maxHealth);
    }

	
}
