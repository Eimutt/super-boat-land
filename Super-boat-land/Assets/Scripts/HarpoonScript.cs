using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonScript : MonoBehaviour {
    // lifetime in  seconds
    private float maxLifeTime = 0.5f;
    private float currentLifeTime = 0f;
    private BoxCollider boxCollider;
    public GameObject WaterPrefab;
    public GameObject RopePrefab;
    public AudioClip SplashSoundEffect;
    GameObject rope;
    GameObject playerBoat;
    AudioSource audioSource;

    bool collidingWithFish = false;
    bool catchingFish = false;
    GameObject currentCatch;
    // Start is called before the first frame update
    void Start() {
        print(boxCollider);
        boxCollider = GetComponent<BoxCollider>();
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
            rope.GetComponent<LineRenderer>().SetPosition(1, currentCatch.transform.position);
            if(currentCatch.tag == "Bomb")
            {
                currentCatch.gameObject.GetComponent<Bomb>().Hook();
            } else
            {
                currentCatch.GetComponent<Animator>().SetBool("isHooked", true);
            }
        } else {
            if (currentLifeTime >= maxLifeTime) {
                GameObject waterParticles = Instantiate(WaterPrefab, transform);
                waterParticles.transform.parent = transform.parent;
                waterParticles.transform.rotation = Quaternion.Euler(-90, 0, 0);
                //audioSource.PlayOneShot(SplashSoundEffect);

                if (collidingWithFish) {
                    print(currentCatch);
                    rope.GetComponent<LineRenderer>().SetPosition(1, currentCatch.transform.position);
                    catchingFish = true;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                } else {
                    DestroySelf();
                }
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "SquidBossTrigger")
        {
            col.gameObject.GetComponent<bossTrigger>().StartBossFight();
            DestroySelf();
        } else if(col.gameObject.tag == "Bomb" && !catchingFish){
            currentCatch = col.gameObject;
            collidingWithFish = true;
        } else if (col.gameObject.tag == "Fish") {
            if (!catchingFish)
            {
                collidingWithFish = true;
                currentCatch = col.gameObject;
            }
        }
        //Debug.Log("GameObject1 collided with " + col.name);
    }
    void OnTriggerExit(Collider col) {
        if (!catchingFish) {
            collidingWithFish = false;
            currentCatch = null;
        }
        //Debug.Log("GameObject1 collided with " + col.name);
    }

    public GameObject getRope()
    {
        return rope;
    }

    void DestroySelf()
    {
        Destroy(rope);
        Destroy(gameObject);
    }

    public void TryDestroySelf()
    {
        if (catchingFish)
        {
            if(currentCatch.tag == "Bomb")
            {
                currentCatch.GetComponent<Bomb>().UnHook();
            } else //Fish
            {
                currentCatch.GetComponent<fishController>().UnHook();
            }
            DestroySelf();
        }
    }


}
