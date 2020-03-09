using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    private bool hooked;
    public float hookSpeed;
    private GameObject player;
    public GameObject explosionEffect;
    private bool moving;
    private Vector3 p0;
    private Vector3 p1;
    private Vector3 p2;
    public float arcDuration;
    private float tsum;
    AudioSource audioSource;
    public AudioClip explosionSound;



    void Start() {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (moving) {
            tsum += Time.deltaTime;
            gameObject.transform.position = Bezier(tsum / arcDuration);
            if (tsum / arcDuration > 1) {
                moving = false;
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }
        if (hooked) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, hookSpeed * Time.deltaTime);
        }
    }

    public void Hook() {
        hooked = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UnHook() {
        hooked = false;
    }

    public void Explode() {
        Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
        if (audioSource != null) {
            //audioSource.PlayOneShot(explosionSound);
        }
        Destroy(gameObject);
    }

    public void SetCurve(Vector3 start, Vector3 mid, Vector3 end) {
        p0 = start;
        p1 = mid;
        p2 = end;
        moving = true;
    }

    public Vector3 Bezier(float t) {
        Vector3 position = (Mathf.Pow(1 - t, 2) * p0 + 2 * t * (1 - t) * p1 + Mathf.Pow(t, 2) * p2);
        return position;
    }
}
