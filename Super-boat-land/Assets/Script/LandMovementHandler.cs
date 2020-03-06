using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMovementHandler : MonoBehaviour
{
    public float speedCoefficient;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        characterController.Move(new Vector3(speedCoefficient * horizontal, speedCoefficient * vertical, 0));


        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            AttackCommand();
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            RecallCommand();
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
