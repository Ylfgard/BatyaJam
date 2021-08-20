using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    private const float defaultRunSpeedMutiplier = 1f;

    [SerializeField]
    private float walkSpeed = 200f;
    [SerializeField]
    private float runSpeedMultiplier = 1.5f;

    private Rigidbody rb;
    private Vector3 velocity;
    private bool isRunning;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = velocity * walkSpeed * (isRunning ? runSpeedMultiplier : defaultRunSpeedMutiplier) * Time.deltaTime + new Vector3(0.0f, rb.velocity.y, 0.0f);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;
    }

    public void SetMoveVelocity(Vector3 velocityVector)
    {
        velocity = velocityVector;
    }
}
