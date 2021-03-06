using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraPositionAnchor;

    [HideInInspector]
    public Vector3 moveDirection { get; private set; }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 camXAxis = cameraPositionAnchor.transform.right;
        Vector3 camZAxis = cameraPositionAnchor.transform.forward;

        moveDirection = camXAxis * horizontalInput + camZAxis * verticalInput;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        
        GetComponent<IMoveVelocity>().SetMoveVelocity(moveDirection);
    }
}
