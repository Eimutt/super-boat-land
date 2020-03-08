using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    private Transform playerPos;
    public float alertRadius;
    public float patrolRadius; 
    public float patrolSpeed;
    public Sprite supriseMark;

    private bool patrollingTowardsPoint;
    private Vector2 patrolTowardsPoint; //The point that the enemy is patrolling towards.
    private float randAngle;
    private float randLength;
    private Vector2 ENEMY_START_POSITION;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        ENEMY_START_POSITION = animator.GetComponent<Enemy>().getStartPosition();
        patrollingTowardsPoint = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float d = 0.0f;
        d = Vector3.Distance(animator.transform.position, playerPos.transform.position);
        if(d < alertRadius)
        {
            //ENEMY WILL CHASE YOU.
            animator.SetBool("isFollowing", true);
        }

        //Code for patroling to random points around inside a circle.
        if(!patrollingTowardsPoint) {
            float angle = Random.Range(0.0f, 2.0f * Mathf.PI);
            //randAngle = Mathf.Sin(Random.Range(0.0f, 2.0f * Mathf.PI));
            randLength = Random.Range(0.0f, patrolRadius);
            patrolTowardsPoint = ENEMY_START_POSITION + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))*randLength;
            patrollingTowardsPoint = true;
        }

        if(patrollingTowardsPoint && Vector2.Distance(patrolTowardsPoint, animator.transform.position) > 0.1f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolTowardsPoint, patrolSpeed * Time.deltaTime);
        }
        else
        {
            patrollingTowardsPoint = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //patrolTowardsPoint = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
