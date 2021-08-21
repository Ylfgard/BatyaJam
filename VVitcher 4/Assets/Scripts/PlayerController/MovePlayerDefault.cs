using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovePlayerDefault : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseTarget;
    [SerializeField]
    private float playerRotationSpeed = 8f;

    //private Camera mainCam;
    private PlayerMain playerMainScript;
    private MoveVelocity moveVelocityScript;
    private MovePlayer movePlayerScript;

    private void Start()
    {
        //mainCam = Camera.main;
        playerMainScript = GetComponent<PlayerMain>();
        moveVelocityScript = GetComponent<MoveVelocity>();
        movePlayerScript = GetComponent<MovePlayer>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(LookDirection(), Vector3.up), playerRotationSpeed);
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
