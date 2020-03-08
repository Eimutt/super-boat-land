using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMovementHandler : MonoBehaviour
{
    public float speedCoefficient;
    private CharacterController characterController;
    private Vector2 moveDirection;
    private SceneSwitch sceneSwitch;
	private Settings Settings;
	public Vector2 Position;
	
	public List<Enemy> enemiesInRange = new List<Enemy>();
	//public List<Enemy> enemiesKilled = new List<Enemy>();
	public List<GameObject> enemiesKilled = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

		Settings = GameObject.FindObjectsOfType<Settings>()[0];

        characterController = GetComponent<CharacterController>();
        sceneSwitch = GetComponent<SceneSwitch>();
    }
	
    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveDirection *= speedCoefficient;

        characterController.Move(moveDirection * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Joystick1Button0 )|| Input.GetKeyDown(KeyCode.X))
            AttackCommand();
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.C))
            RecallCommand();

        else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            sceneSwitch.SwitchScene("BoatScene", false);
		Position = transform.position;
		List<Enemy> enemiesNotNull = Settings.EnemyManager.GetEnemies();
		
		enemiesNotNull.RemoveAll(item => item == null);
		
		//Targeting code
		foreach (Enemy enemy in Settings.EnemyManager.GetEnemies())
		{

			float distance = (enemy.transform.position - transform.position).magnitude;
			if (distance < Settings.AttackRange){
				if (!enemiesInRange.Contains(enemy)){
					enemiesInRange.Add(enemy);	
				}
			}else {enemiesInRange.Remove(enemy);}
			
			//Debug.Log(Settings.AttackRange);
		}
		enemiesInRange.RemoveAll(item => item == null);
		/*
		foreach (Enemy enemy in enemiesInRange)
		{
			Debug.Log(enemy);
		}
		*/
		
		foreach (GameObject enemy in enemiesKilled)
		{
			//Debug.Log(enemy);
		}
		
		
    }
	public List<Enemy> getEnemiesInRange(){
		return enemiesInRange;
	}
	public List<GameObject> getEnemiesDead(){
		return enemiesKilled;
	}
	public void enemyKilled(GameObject enemy){
		enemiesKilled.Add(enemy);
	}
	
	public Vector2 getPosition(){
		return Position;
	}
	
    void AttackCommand()
    {
		foreach ( Crew crew in Settings.CrewManager.getCrew()){
			crew.attacking = true;
		}
        print("Attack command");
    }

    void RecallCommand()
    {
		foreach ( Crew crew in Settings.CrewManager.getCrew()){
			crew.attacking = false;
		}
        print("Recall command");
    }
}
