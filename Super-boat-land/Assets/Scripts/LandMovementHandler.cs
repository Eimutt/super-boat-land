using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMovementHandler : MonoBehaviour
{
    public float speedCoefficient;
    private CharacterController characterController;
    private Vector2 moveDirection;
    private SceneSwitch sceneSwitch;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        sceneSwitch = GetComponent<SceneSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveDirection *= speedCoefficient;

        characterController.Move(moveDirection * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            AttackCommand();
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            RecallCommand();

        else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            sceneSwitch.SwitchScene("BoatScene", false);
    }

    void AttackCommand()
    {
        print("Attack command");
    }

    void RecallCommand()
    {
        print("Recall command");
    }
}
