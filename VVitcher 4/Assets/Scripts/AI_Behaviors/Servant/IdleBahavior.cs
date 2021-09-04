using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBahavior : StateMachineBehaviour {
    private float _startFollowDistance;
    private Transform _player;
    //private FMOD.Studio.EventInstance instance;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _startFollowDistance = animator.GetComponent<ServantStats>().startFollowDistance;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToPlayer = Vector3.Distance(animator.transform.position, _player.position);
        if(distanceToPlayer < _startFollowDistance)
        {
            animator.SetBool("isFollowing", true);

            //instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            //instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/agr_servants");
            //instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(animator.gameObject));
            //instance.start();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
