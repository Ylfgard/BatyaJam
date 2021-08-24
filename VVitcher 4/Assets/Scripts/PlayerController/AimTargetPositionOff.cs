using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTargetPositionOff : MonoBehaviour
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
        animator.SetLookAtWeight(0, 0, 0, 0, 0);
        animator.SetLookAtPosition(aimTargetOffset.position);
    }
}
