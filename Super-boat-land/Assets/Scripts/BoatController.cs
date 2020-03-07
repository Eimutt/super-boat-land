using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 2.0f;

    private CharacterController controller;
    private Vector2 moveDirection;

    private SceneSwitch sceneSwitch;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        sceneSwitch = GetComponent<SceneSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveDirection *= speed;

        controller.Move(moveDirection * Time.deltaTime);

         if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            sceneSwitch.SwitchScene("LandScene", true);
    }
}
