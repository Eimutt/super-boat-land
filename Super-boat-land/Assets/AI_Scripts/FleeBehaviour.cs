using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeBehaviour : StateMachineBehaviour
{
    private Transform playerPos;
    private Vector2 fleePosition;
    private float angle;
    private Vector2 moveDirection;

    private float lifeTime;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //escape here
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        fleePosition = -1 * playerPos.position * 1000;
        moveDirection = new Vector2(fleePosition.x - animator.transform.position.x, fleePosition.y - animator.transform.position.y);
        angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        lifeTime = 1.0f;
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, fleePosition, 3 * Time.deltaTime);
        animator.transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0.0f)
        {
            Destroy(animator.gameObject);
        }
    }

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
