using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishController : MonoBehaviour
{
    public float escapeSpeed;


    private float alertRadius;
    private Transform playerPos;
    public Animator animator;
    // Start is called before the first frame update
    private Vector2 fleePosition;
    private float angle;
    private Vector2 moveDirection;
    private bool escape;

    //These variables are for the fish when it is hooked.
    //When resetDirection is true, the fish will generate a new angle that it will move towards.
    private bool resetDirection;
    private float resetTime;

    void Start()
    {
        alertRadius = 0.5f;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        escape = false;
        resetDirection = true;
        resetTime = -0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = 0.0f;
        d = Vector3.Distance(transform.position, playerPos.transform.position);
        if ((d < alertRadius || escape) && !animator.GetBool("isHooked"))
        {
            //ENEMY WILL CHASE YOU.
            animator.SetBool("flee", true);
            if (!escape)
            {
                fleePosition = -1 * playerPos.position * 1000;
                moveDirection = new Vector2(fleePosition.x - transform.position.x, fleePosition.y - transform.position.y);
                angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            }
            animator.transform.position = Vector2.MoveTowards(transform.position, fleePosition, 3 * Time.deltaTime);
            animator.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            escape = true;
        }

        /*
         *  Capture the fish while it will try to escape.
         */
        if(animator.GetBool("isHooked"))
        {
            if(resetTime > 0.0f)
            {
                Vector2 forward = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle))*3;
                moveDirection = new Vector2(transform.position.x - forward.x, transform.position.y - forward.y);
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                resetTime -= 0.05f;
                transform.position = Vector2.MoveTowards(transform.position,  moveDirection, escapeSpeed * Time.deltaTime);
                //The player boat is also pushed a little bit.
                playerPos.position = Vector2.MoveTowards(playerPos.position, transform.position, escapeSpeed * 0.9f * Time.deltaTime);
            }
            else
            {
                fleePosition = playerPos.position * 1000;
                Vector2 tempMoveDir = new Vector2(playerPos.transform.position.x - transform.position.x, playerPos.transform.position.y - transform.position.y);
                resetTime = Random.Range(2.0f, 10.0f);
                angle = Mathf.Atan2(tempMoveDir.y, tempMoveDir.x) * Mathf.Rad2Deg + Random.Range(-30, 30);
            }

        }
    }
}
