﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMovementHandler : MonoBehaviour {
    public float speedCoefficient;
    private CharacterController characterController;
    private Vector2 moveDirection;
    private SceneSwitch sceneSwitch;
    private Settings Settings;
    public Vector2 Position;
    public Animator animator;
    public bool facingRight = true;
    public List<Enemy> enemiesInRange = new List<Enemy>();
    //public List<Enemy> enemiesKilled = new List<Enemy>();
    public List<GameObject> enemiesKilled = new List<GameObject>();


    // Start is called before the first frame update
    void Start() {

        Settings = GameObject.FindObjectsOfType<Settings>()[0];

        characterController = GetComponent<CharacterController>();
        sceneSwitch = GetComponent<SceneSwitch>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveDirection *= speedCoefficient;

        if (moveDirection.x > 0) {
            animator.SetBool("FaceRight", true);
        } else if (moveDirection.x < 0) {
            animator.SetBool("FaceRight", false);
        }

        if (moveDirection.magnitude > 0) {
            animator.SetBool("Walking", true);
        } else {
            animator.SetBool("Walking", false);
        }

        characterController.Move(moveDirection * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.X))
            AttackCommand();
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.C))
            RecallCommand();

        else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            sceneSwitch.SwitchScene("BoatScene", false);
        Position = transform.position;
		foreach (Enemy enemy in enemiesInRange)
		{
			
			Debug.Log(enemy);
		}
		enemiesInRange.RemoveAll(item => item == null);
    }

    public Vector2 getPosition() {
        return Position;
    }

    void AttackCommand() {
		enemiesInRange.RemoveAll(item => item == null);
        foreach (Crew crew in Settings.CrewManager.getCrew()) {
            crew.attacking = true;
        }
        Position = transform.position;
        List<Enemy> enemiesNotNull = Settings.EnemyManager.GetEnemies();

        enemiesNotNull.RemoveAll(item => item == null);

        //Targeting code
        foreach (Enemy enemy in Settings.EnemyManager.GetEnemies()) {

            float distance = (enemy.transform.position - transform.position).magnitude;
            if (distance < Settings.AttackRange) {
                if (!enemiesInRange.Contains(enemy)) {
                    enemiesInRange.Add(enemy);
                }
            } else { enemiesInRange.Remove(enemy); }

            //Debug.Log(Settings.AttackRange);
        }
		
        enemiesInRange.RemoveAll(item => item == null);
		enemiesInRange.RemoveAll(item => item.Equals(null));
        
		
		

        foreach (GameObject enemy in enemiesKilled) {
            //Debug.Log(enemy);
        }


    }
    public List<Enemy> getEnemiesInRange() {
		enemiesInRange.RemoveAll(item => item == null);
        return enemiesInRange;
    }
    public List<GameObject> getEnemiesDead() {
        return enemiesKilled;
    }
    public void enemyKilled(GameObject enemy) {
        enemiesKilled.Add(enemy);
    }

    void RecallCommand() {
        foreach (Crew crew in Settings.CrewManager.getCrew()) {
            crew.attacking = false;
        }
        print("Recall command");
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "GoToBoat") {
            sceneSwitch.SwitchScene("KawajiSea", true);
        }
    }
}
