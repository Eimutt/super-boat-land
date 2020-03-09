using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 startPosition; //This should never change
    public Vector2 Position; //This should never change
    public EnemyManager EnemyManager { get; set; }
    public Settings Settings { get; set; }
    private int health;
    public int maxHealth = 1000;
    public static bool isAlive = true;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        health = maxHealth;
        Settings = FindObjectsOfType<Settings>() [0];
        EnemyManager = FindObjectsOfType<EnemyManager>() [0];
        Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateEnemy(float deltaTime)
    {
        Position = transform.position;
		//Debug.Log(Settings.CrewManager.getCrew());
		foreach (Crew crew in Settings.CrewManager.getCrew())
		{

			if ((this.transform.position-crew.transform.position).magnitude < Settings.HurtRange && !crew.Equals(null)){
				
				//Debug.Log("trying to attack");
				if(crew.damage(10)){
					Destroy(crew.gameObject);
					//enemiesKilled.Add(enemy);
					//enemiesInRange.Remove(enemy);
				}
			}

		}
    }

    /*
     *  This is used for the AI to generate a random position.
     */
    public Vector2 getStartPosition()
    {
        return startPosition;
    }

    public bool damage(int damage)
    {
        setHealth(getHealth() - damage);
        //health -=damage;
        Debug.Log(getHealth());
        if (getHealth() <= 0)
        {
            isAlive = false;
            //Settings.Captain.enemyKilled(this.gameObject);

            //Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    private void setHealth(int newHealth)
    {
        health = newHealth;
        healthBar.SetHealth(health / maxHealth);
    }

    public int getHealth()
    {
        return health;
    }

    public bool getIsAlive()
    {
        return isAlive;
    }
}
