using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishController : MonoBehaviour
{
    private float alertRadius;
    private Transform playerPos;
    public Animator animator;
    // Start is called before the first frame update
    private Vector2 fleePosition;
    private float angle;
    private Vector2 moveDirection;
    private bool escape;

    void Start()
    {
        alertRadius = 0.5f;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        fleePosition = -1 * playerPos.position * 1000;
        moveDirection = new Vector2(fleePosition.x - animator.transform.position.x, fleePosition.y - animator.transform.position.y);
        angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        escape = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = 0.0f;
        d = Vector3.Distance(transform.position, playerPos.transform.position);
        if (d < alertRadius || escape)
        {
            //ENEMY WILL CHASE YOU.
            animator.SetBool("flee", true);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, fleePosition, 3 * Time.deltaTime);
            animator.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            escape = true;
        }
    }
}
