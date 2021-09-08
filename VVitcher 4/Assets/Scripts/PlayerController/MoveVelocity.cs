using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    private const float defaultSpeed = 1f;
    private const float normalGravity = -9.81f;
    //[SerializeField] private float groundCheckRadius = 0.22f;

    [SerializeField]
    private GameObject groundCheck;
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
    private CameraModeChanger cameraModeChangerScript;

    private Rigidbody rb;
    private Vector3 dirVector;
    private float _runningTimer, _rollbackTimer;
    private bool _canRun = true;
    private bool _isRunning, _isCooldown, _isRollback;
    //private bool _isGrounded, _isHighGravity;
    RaycastHit hit;

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
        cameraModeChangerScript = GetComponent<CameraModeChanger>();

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (playerMainScript.isDead) return;

        //GroundCheck();

        if (dirVector.magnitude > 0)
        {
            //if (_isGrounded && _isHighGravity)
            //{
            //    Physics.gravity = new Vector3(0, normalGravity, 0);
            //    _isHighGravity = false;
            //    Debug.Log("Low...");
            //}
            //else if (!_isGrounded && !_isHighGravity)
            //{
            //    Physics.gravity *= 2;
            //    _isHighGravity = true;
            //    Debug.Log("HIGH!");
            //}

            rb.velocity = dirVector * walkSpeed * (isRunning ? runSpeedMultiplier : defaultSpeed) * Time.fixedDeltaTime + new Vector3(0, rb.velocity.y, 0);
        }
    }

    //private void GroundCheck()
    //{
    //    _isGrounded = Physics.SphereCast(groundCheck.transform.position, groundCheckRadius, Vector3.down, out hit, 0, 12, QueryTriggerInteraction.Ignore);
    //    Debug.Log("Ground checked: " + hit.point);
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(hit.point, groundCheckRadius);
    //    Gizmos.color = Color.white;
    //}

    public void SetMoveVelocity(Vector3 moveDirection)
    {
        dirVector = moveDirection;
    }

    private void Update()
    {
        PlayerRun();
    }

    private void PlayerRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRun && cameraModeChangerScript.currentCameraMode != CameraMode.WalkMode)
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

            cameraModeChangerScript.SetPlayerRunAimMode();
            Physics.gravity *= 2;
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
        if(cameraModeChangerScript.currentCameraMode == CameraMode.RunAimMode)
            cameraModeChangerScript.ChangeMode();

        Physics.gravity = new Vector3(0, normalGravity, 0);
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
