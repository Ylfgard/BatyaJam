using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTargetPosition : MonoBehaviour
{
    [SerializeField]
    private Transform aimTargetOffset;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        //animator.SetLookAtWeight(.5f, .5f, .5f, .5f, .5f);
        //animator.SetLookAtWeight(0, 0, 0, 0, 0);

        animator.SetLookAtWeight(0.5f, 0.5f, 0.5f, 0.5f, 0.5f);
        animator.SetLookAtPosition(aimTargetOffset.position);
    }
}
