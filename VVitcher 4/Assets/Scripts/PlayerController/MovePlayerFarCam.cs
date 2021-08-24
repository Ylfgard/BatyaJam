using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovePlayerFarCam : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseTarget;
    [SerializeField]
    private float playerRotationSpeed = 8f;

    private Rigidbody rb;
    private PlayerMain playerMainScript;
    private MoveVelocity moveVelocityScript;
    private MovePlayer movePlayerScript;
    private PlayerAnimationStateController playerAnimationStateControllerScript;

    private const float rotationTreshold = 0.01f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMainScript = GetComponent<PlayerMain>();
        moveVelocityScript = GetComponent<MoveVelocity>();
        movePlayerScript = GetComponent<MovePlayer>();
        playerAnimationStateControllerScript = GetComponent<PlayerAnimationStateController>();
    }

    private void Update()
    {
        if (playerMainScript.isDead) return;

        Quaternion rot = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(LookDirection(), Vector3.up), playerRotationSpeed * Time.deltaTime);
        
        if(playerAnimationStateControllerScript.isAgressive)
        {
            transform.rotation = rot;
        }
        else
        {
            if (rb.velocity.magnitude > rotationTreshold)
                transform.rotation = rot;
        }
    }

    public Vector3 LookDirection()
    {
        if (moveVelocityScript.isRunning)
        {
            Vector3 lookRotation = movePlayerScript.moveDirection;
            //Vector3 lookRotation = new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
            return lookRotation;
        }

        Vector3 lookAt = new Vector3(mouseTarget.transform.position.x, transform.position.y, mouseTarget.transform.position.z);
        Vector3 direction = lookAt - transform.position;
        return direction;
    }
}
