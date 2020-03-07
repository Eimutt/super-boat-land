using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour {
    public float speed = 2.0f;
    private float harpoonForce = 9;
    private float maxHarpoonCD = 1;
    private float currentHarpoonCD = 0;

    private CharacterController controller;
    public GameObject HarpoonPrefab;
    private Vector2 moveDirection;
    private Vector2 latestNonZeroMoveDir;

    private SceneSwitch sceneSwitch;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
        sceneSwitch = GetComponent<SceneSwitch>();
    }

    // Update is called once per frame
    void Update() {
        if (currentHarpoonCD < maxHarpoonCD) {
            currentHarpoonCD += Time.deltaTime;
        }

        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveDirection.normalized != Vector2.zero) {
            latestNonZeroMoveDir = moveDirection;
        }
        moveDirection *= speed;

        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            sceneSwitch.SwitchScene("LandScene", true);

        if (Input.GetKeyDown(KeyCode.Q) && currentHarpoonCD >= maxHarpoonCD) {
            shootHarpoon();
            currentHarpoonCD = 0;
        }
    }

    // Shoot harpoon for fishing
    void shootHarpoon() {
        Vector3 directionVector = Vector3.Normalize(latestNonZeroMoveDir);
        print(directionVector);
        GameObject harpoon = Instantiate(HarpoonPrefab, transform);

        Vector3 dir = directionVector;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        harpoon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        harpoon.transform.parent = transform.parent;
        harpoon.GetComponent<Rigidbody2D>().AddForce(harpoonForce * directionVector);
    }

}
