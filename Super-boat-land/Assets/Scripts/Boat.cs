using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {
    public float health;
    private SceneSwitch sceneSwitch;
    // Start is called before the first frame update
    void Start() {
        sceneSwitch = GetComponent<SceneSwitch>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void TakeDamage(int damage) {
        print("Boat takes " + damage + " damage!");
        health -= damage;
        if (health <= 0) {
            print("you died");
            gameObject.GetComponent<SceneSwitch>().ResetScene();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Bomb") {
            other.GetComponent<Bomb>().Explode();
            TakeDamage(20);
        }

        if (other.tag == "DesertIsland") {
            sceneSwitch.SwitchScene("KawajiDesert", true);
        } else if (other.tag == "SwampIsland") {
            sceneSwitch.SwitchScene("GrassLandFinal", true);
        }
    }
}
