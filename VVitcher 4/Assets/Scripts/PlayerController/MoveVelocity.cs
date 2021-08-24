using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    private const float defaultSpeed = 1f;

    [SerializeField]
    private float walkSpeed = 200f;
    [SerializeField]
    private float runSpeedMultiplier = 1.5f;
    [SerializeField]
    private float runContinuance = 1f;
    [SerializeField]
    private float runCooldown = 1f;

    private PlayerMain playerMainScript;
    private PlayerAnimationStateController playerAnimationStateControllerScript;
    private Rigidbody rb;
    private Vector3 velocity;
    private bool _canRun = true;
    private bool _isRunning;
    private bool _isCooldown;
    private float _timerOfRun;

    public bool canRun
    {
        get
        {
            //if (!playerAnimationStateControllerScript.isAgressive) return false;
            return _canRun;
        }
        set { _canRun = value; }
    }
    public bool isRunning
    {
        get { return _isRunning; }
        private set { _isRunning = value; }
    }

    private void Start()
    {
        playerMainScript = GetComponent<PlayerMain>();
        playerAnimationStateControllerScript = GetComponent<PlayerAnimationStateController>();

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (playerMainScript.isDead) return;

        if(velocity.magnitude > 0)
            rb.velocity = velocity * walkSpeed * (isRunning ? runSpeedMultiplier : defaultSpeed) * Time.fixedDeltaTime + new Vector3(0.0f, rb.velocity.y, 0.0f);
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
            _timerOfRun = Time.time + runContinuance;
            Debug.Log("Run!");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isRunning)
        {
            isRunning = false;
            _isCooldown = true;
            _timerOfRun = Time.time + runCooldown;
        }

        if (isRunning)
        {
            if (!playerAnimationStateControllerScript.isAgressive || _timerOfRun < Time.time)
            {
                AbortRunning();
            }
        }

        if (_isCooldown && _timerOfRun < Time.time)
        {
            _isCooldown = false;
            canRun = true;
        }
    }

    private void AbortRunning()
    {
        isRunning = false;
        _isCooldown = true;
        _timerOfRun = Time.time + runCooldown;
    }
}
