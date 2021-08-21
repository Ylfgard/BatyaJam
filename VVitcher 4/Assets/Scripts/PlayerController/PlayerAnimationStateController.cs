using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationStateController : MonoBehaviour
{
    private Animator animator;

    //private int isWalkingForwardHash = Animator.StringToHash("isWalkingForward_b");
    //private int isWalkingBackwardHash = Animator.StringToHash("isWalkingBackward_b");
    //private int isStrafingToLeftHash = Animator.StringToHash("isStrafingToLeft_b");
    //private int isStrafingToRightHash = Animator.StringToHash("isStrafingToRight_b");

    private int velocityXHash = Animator.StringToHash("velocityX_f");
    private int velocityZHash = Animator.StringToHash("velocityZ_f");

    private Vector3 playerVelocity;
    private MovePlayer movePlayerScript;
    private MoveVelocity moveVelocityScript;

    private float defAnimSpeed;
    private float animSpeedMultiplier = 2f;
    private bool isAnimSpeeded;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movePlayerScript = GetComponent<MovePlayer>();
        moveVelocityScript = GetComponent<MoveVelocity>();

        defAnimSpeed = animator.speed;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W)) animator.SetBool(isWalkingForwardHash, true);
        //if (Input.GetKeyUp(KeyCode.W)) animator.SetBool(isWalkingForwardHash, false);

        //if (Input.GetKeyDown(KeyCode.A)) animator.SetBool(isStrafingToLeftHash, true);
        //if (Input.GetKeyUp(KeyCode.A)) animator.SetBool(isStrafingToLeftHash, false);

        //if (Input.GetKeyDown(KeyCode.S)) animator.SetBool(isWalkingBackwardHash, true);
        //if (Input.GetKeyUp(KeyCode.S)) animator.SetBool(isWalkingBackwardHash, false);

        //if (Input.GetKeyDown(KeyCode.D)) animator.SetBool(isStrafingToRightHash, true);
        //if (Input.GetKeyUp(KeyCode.D)) animator.SetBool(isStrafingToRightHash, false);

        //IncreaseAnimSpeedWhenRunning();

        Vector3 vs = movePlayerScript.moveDirection;

        float velocityX = Vector3.Dot(vs.normalized, transform.right);
        float velocityZ = Vector3.Dot(vs.normalized, transform.forward);

        animator.SetFloat(velocityXHash, velocityX, 0.1f, Time.deltaTime);
        animator.SetFloat(velocityZHash, velocityZ, 0.1f, Time.deltaTime);
    }

    private void IncreaseAnimSpeedWhenRunning()
    {
        if (moveVelocityScript.isRunning && !isAnimSpeeded)
            Debug.Log("Speed up anim");
            //animator.speed *= animSpeedMultiplier;
        if (!moveVelocityScript.isRunning && isAnimSpeeded)
            Debug.Log("Speed down anim");
        //animator.speed = defAnimSpeed;
    }
}
