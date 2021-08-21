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
    private bool _canRun = true;
    private bool _isRunning;
    private bool _isCooldown;
    private float _timerOfRun;

    public bool isRunning
    {
        get { return _isRunning; }
        private set { _isRunning = value; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * walkSpeed * (_isRunning ? runSpeedMultiplier : defaultRunSpeedMutiplier) * Time.fixedDeltaTime + new Vector3(0.0f, rb.velocity.y, 0.0f);
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canRun)
        {
            _canRun = false;
            _isRunning = true;
            _timerOfRun = Time.time + runContinuance;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && _isRunning)
        {
            _isRunning = false;
            _isCooldown = true;
            _timerOfRun = Time.time + runCooldown;
        }

        if (_isRunning && _timerOfRun < Time.time)
        {
            _isRunning = false;
            _isCooldown = true;
            _timerOfRun = Time.time + runCooldown;
        }

        if (_isCooldown && _timerOfRun < Time.time)
        {
            _isCooldown = false;
            _canRun = true;
        }
    }
}
