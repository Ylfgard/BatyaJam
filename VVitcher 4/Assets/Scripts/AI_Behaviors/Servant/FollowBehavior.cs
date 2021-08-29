using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowBehavior : StateMachineBehaviour {

    private Transform _player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player.GetComponent<PlayerMain>().isDead)
        {
            animator.SetBool("playerDead", true);
        }

        NavMeshAgent agent = animator.GetComponent<NavMeshAgent>();
        animator.speed = agent.velocity.magnitude / agent.speed;

        if (Vector3.Distance(animator.transform.position, _player.position) < animator.GetComponent<ServantStats>().attackRange)
        {
            animator.SetTrigger("attack");
        }
        animator.GetComponent<NavMeshAgent>().SetDestination(_player.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<NavMeshAgent>().isStopped = true;
        animator.GetComponent<NavMeshAgent>().ResetPath();

        animator.speed = 1;
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
