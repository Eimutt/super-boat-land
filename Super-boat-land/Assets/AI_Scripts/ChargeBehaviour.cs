using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBehaviour : StateMachineBehaviour
{
    public float chargeDuration;
    private float ogDuration;
    public GameObject ChargeRing; //The ring that visualizes frog attack.
    private GameObject chargeRingCopy;
    private bool charge;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ogDuration = chargeDuration;
        charge = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chargeDuration -= Time.deltaTime;
        //Debug.Log(chargeDuration);
        if(chargeDuration/ogDuration<0.90f && !charge)
        {

            chargeRingCopy = Instantiate(ChargeRing, animator.transform.position, Quaternion.identity);
            charge = true;

        }
        if(charge)
        {
            //SET THE TIME VARIABLE INSIDE THE SHADER TO MAKE IT ANIMATE.
            Renderer rend = chargeRingCopy.GetComponent<Renderer>();
            float fraction = Mathf.Min(1.0f, 1-(chargeDuration / ogDuration));
            rend.material.SetFloat("_TimeFraction", fraction);
        }
        //Charges the attack for a few seconds before firing off.
        if (chargeDuration < 0.0f)
        {
            chargeRingCopy.SetActive(false);
            animator.transform.GetComponent<Attack>().AOEAttack();
            animator.SetBool("isCharging", false);
            chargeDuration = int.MaxValue;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chargeDuration = ogDuration;
        Destroy(chargeRingCopy);
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
