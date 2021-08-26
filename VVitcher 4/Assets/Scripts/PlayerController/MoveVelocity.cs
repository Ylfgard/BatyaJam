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
    private Vector3 dirVector;
    private bool _canRun = true;
    private bool _isRunning, _isCooldown, _isRollback;
    private float _runningTimer, _rollbackTimer;

    public bool canRun
    {
        get { return _canRun; }
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

        if (dirVector.magnitude > 0)
            rb.velocity = dirVector * walkSpeed * (isRunning ? runSpeedMultiplier : defaultSpeed) * Time.fixedDeltaTime + new Vector3(0, rb.velocity.y, 0);
    }

    private void Update()
    {
        PlayerRun();
    }

    public void SetMoveVelocity(Vector3 moveDirection)
    {
        dirVector = moveDirection;
    }

    private void PlayerRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRun)
        {
            if (_isRollback && _rollbackTimer - Time.time > 0)
            {
                float t = _rollbackTimer - Time.time;
                _runningTimer = runContinuance - t + Time.time;
            }
            else
            {
                _runningTimer = Time.time + runContinuance;
            }

            _isRollback = false;
            isRunning = true;
            canRun = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isRunning)
        {
            AbortRunning();
        }

        if (isRunning)
        {
            if (!playerAnimationStateControllerScript.isAgressive || _runningTimer < Time.time)
            {
                AbortRunning();
            }
        }

        if (_isCooldown && _runningTimer < Time.time)
        {
            _isCooldown = false;
            canRun = true;
        }
    }

    private void AbortRunning()
    {
        isRunning = false;

        float t = runContinuance - (_runningTimer - Time.time);

        if (runContinuance / 2 > t )
        {
            _rollbackTimer = Time.time + t;
            _isRollback = true;
            canRun = true;
        }
        else
        {
            _runningTimer = Time.time + runCooldown;
            _isCooldown = true;
        }
    }
}
