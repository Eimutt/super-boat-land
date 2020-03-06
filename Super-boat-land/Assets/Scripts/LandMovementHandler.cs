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
    // Start is called before the first frame update
    void Start()
    {
		//Settings = GetComponent<Settings>();
		Settings = GameObject.FindObjectsOfType<Settings>()[0];
		Debug.Log(Settings);
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
