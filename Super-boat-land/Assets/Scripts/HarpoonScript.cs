using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonScript : MonoBehaviour {
    // lifetime in  seconds
    private float maxLifeTime = 1.5f;
    private float currentLifeTime = 0f;
    private BoxCollider2D boxCollider;
    public GameObject WaterPrefab;
    public GameObject RopePrefab;
    public AudioClip SplashSoundEffect;
    GameObject rope;
    GameObject playerBoat;
    AudioSource audioSource;

    bool collidingWithFish = false;
    bool catchingFish = false;
    GameObject currentFish;
    // Start is called before the first frame update
    void Start() {
        print(boxCollider);
        boxCollider = GetComponent<BoxCollider2D>();
        rope = Instantiate(RopePrefab);
        playerBoat = GameObject.FindGameObjectWithTag("Player");
        rope.transform.parent = transform.parent;
        rope.GetComponent<LineRenderer>().SetPosition(0, playerBoat.transform.position);
        rope.GetComponent<LineRenderer>().SetPosition(1, transform.position);
        //audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        rope.GetComponent<LineRenderer>().SetPosition(0, playerBoat.transform.position);
        rope.GetComponent<LineRenderer>().SetPosition(1, transform.position);

        currentLifeTime += Time.deltaTime;

        if (catchingFish) {
            rope.GetComponent<LineRenderer>().SetPosition(1, currentFish.transform.position);
        } else {
            if (currentLifeTime >= maxLifeTime) {
                GameObject waterParticles = Instantiate(WaterPrefab, transform);
                waterParticles.transform.parent = transform.parent;
                waterParticles.transform.rotation = Quaternion.Euler(-90, 0, 0);
                //audioSource.PlayOneShot(SplashSoundEffect);

                if (collidingWithFish) {
                    print(currentFish);
                    rope.GetComponent<LineRenderer>().SetPosition(1, currentFish.transform.position);
                    catchingFish = true;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                } else {
                    Destroy(rope);
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "SquidBossTrigger")
        {
            col.gameObject.GetComponent<bossTrigger>().StartBossFight();
            Destroy(rope);
            Destroy(gameObject);
        } else
        {
            if (!catchingFish)
            {
                collidingWithFish = true;
                currentFish = col.gameObject;
            }
        }

        //Debug.Log("GameObject1 collided with " + col.name);
    }
    void OnTriggerExit2D(Collider2D col) {
        if (!catchingFish) {
            collidingWithFish = false;
            currentFish = null;
        }
        //Debug.Log("GameObject1 collided with " + col.name);
    }
}
