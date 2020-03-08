using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public float lifeTime;
    public float attackDelay;
    private bool emerged;
    private float timer;
    private BoxCollider boxCollider;
    public Sprite tentacleSprite;
    private SpriteRenderer spriteRenderer;
    private bool damaged;
    public int tentacleDamage;
    public bool decorative;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * Random.Range(0.8f, 1.2f);
        if(timer > attackDelay && !emerged)
        {
            Emerge();
        }
        if(timer > lifeTime && !decorative)
        {
            Destroy(gameObject);
        }
    }

    void Emerge()
    {
        emerged = true;
        boxCollider.enabled = true;
        spriteRenderer.sprite = tentacleSprite;
    }

    void OnTriggerStay(Collider other)
    {
        if (!damaged)
        {
            damaged = true;
            other.GetComponent<Boat>().TakeDamage(tentacleDamage);
        }

    }
}
