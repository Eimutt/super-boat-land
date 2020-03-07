using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBehaviour : StateMachineBehaviour
{
    public float chargeDuration;
    private float ogDuration;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ogDuration = chargeDuration;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chargeDuration -= Time.deltaTime;
        Debug.Log(chargeDuration);
        //Charges the attack for a few seconds before firing off.
        if(chargeDuration < 0.0f)
        {
            animator.transform.GetComponent<Attack>().AOEAttack();
            animator.SetBool("isCharging", false);
            chargeDuration = int.MaxValue;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chargeDuration = ogDuration;
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
