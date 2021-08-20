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
    [SerializeField]
    private float runContinuance = 1f;
    [SerializeField]
    private float runCooldown = 1f;

    private Rigidbody rb;
    private Vector3 velocity;
    private bool canRun = true;
    private bool isRunning;
    private bool isCooldown;
    private float timerOfRun;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * walkSpeed * (isRunning ? runSpeedMultiplier : defaultRunSpeedMutiplier) * Time.fixedDeltaTime + new Vector3(0.0f, rb.velocity.y, 0.0f);
    }

    private void Update()
    {
        PlayerRun();
    }

    public void SetMoveVelocity(Vector3 velocityVector)
    {
        velocity = velocityVector;
    }

    private void PlayerRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRun)
        {
            canRun = false;
            isRunning = true;
            timerOfRun = Time.time + runContinuance;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isRunning)
        {
            isRunning = false;
            isCooldown = true;
            timerOfRun = Time.time + runCooldown;
        }

        if (isRunning && timerOfRun < Time.time)
        {
            isRunning = false;
            isCooldown = true;
            timerOfRun = Time.time + runCooldown;
        }

        if (isCooldown && timerOfRun < Time.time)
        {
            isCooldown = false;
            canRun = true;
        }
    }
}
