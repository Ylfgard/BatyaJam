using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleBehavior : StateMachineBehaviour {
    public float reloadTime = 3f;
    public float rotationSpeed = 5f;

    private float _timeRemaining = 0f;

    private Transform _player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeRemaining = reloadTime;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Quaternion lookRotation = Quaternion.LookRotation((_player.position - animator.transform.position).normalized);
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0)
        {
            _timeRemaining = reloadTime;
            float healthPercent = (float)animator.GetComponent<ArchdemonStats>().health / animator.GetComponent<ArchdemonStats>().startHealth;

            if (healthPercent > 0.75f)
            {
                animator.SetTrigger("Stage1");
            }
            if(healthPercent <= 0.75f)
            {
                //int rnd = Random.Range(1, 3);
                //if (rnd == 1)
                //    animator.SetTrigger("Stage1");
                //if (rnd == 2)
                //    animator.SetTrigger("Stage2");
                if (GameObject.Find("LaserBeamsAttack(Clone)") == null)
                {
                    animator.SetTrigger("Stage2");
                }
                else
                {
                    animator.SetTrigger("Stage1");
                }
            }
            //if(healthPercent <= 0.5f)
            //{
            //    if (GameObject.Find("LaserBeamsAttack(Clone)") == null)
            //    {
            //        animator.SetTrigger("Stage2");
            //    }
            //    else
            //    {
            //        //int rnd = Random.Range(1, 3);
            //        //if (rnd == 1)
            //        //    animator.SetTrigger("Stage1");
            //        //if (rnd == 2)
            //        //    animator.SetTrigger("Stage3");
            //        animator.SetTrigger("Stage1");
            //    }
            //}
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
