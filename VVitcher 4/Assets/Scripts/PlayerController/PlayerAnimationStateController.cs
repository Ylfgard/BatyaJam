using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationStateController : MonoBehaviour, IPlayerCameraMode
{
    [SerializeField]
    private GameObject crossbow;
    [SerializeField]
    private float _crossbowEnablerDelay = 0.1f;
    
    private Animator animator;

    private int velocityXHash = Animator.StringToHash("velocityX_f");
    private int velocityZHash = Animator.StringToHash("velocityZ_f");

    private int agressiveStateHash = Animator.StringToHash("isAgressive_b");

    private int fireTriggerHash = Animator.StringToHash("fire_trig");
    private int hitReactTriggerHash = Animator.StringToHash("hitReact_trig");
    private int dyingTriggerHash = Animator.StringToHash("dying_trig");

    //private Vector3 playerVelocity;
    private MovePlayer movePlayerScript;
    private MoveVelocity moveVelocityScript;

    //private float _defAnimSpeed;
    private CameraMode cameraMode;
    private float _speedMultiplier = 2f;
    private float dampTime = 0.1f;
    private bool _isSpeededUp;
    private bool _isAgressive;
    private bool _isModeChanged;

    public bool isAgressive { get { return _isAgressive; } private set { _isAgressive = value; } }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movePlayerScript = GetComponent<MovePlayer>();
        moveVelocityScript = GetComponent<MoveVelocity>();

        //_defAnimSpeed = animator.speed;
    }

    private void Update()
    {
        if (_isModeChanged)
        {
            StopCoroutine(CrossbowGOEnabler());
            
            animator.SetBool(agressiveStateHash, isAgressive);
            StartCoroutine(CrossbowGOEnabler());
            _isModeChanged = false;
        }

        Vector3 directionVector = movePlayerScript.moveDirection;

        float velocityX = Vector3.Dot(directionVector.normalized, transform.right);
        float velocityZ = Vector3.Dot(directionVector.normalized, transform.forward);
        float currDamp = dampTime;

        CheckSpeedUp();
        if(_isSpeededUp)
        {
            velocityX *= _speedMultiplier;
            velocityZ *= _speedMultiplier;
            currDamp *= _speedMultiplier;
        }

        animator.SetFloat(velocityXHash, velocityX, currDamp, Time.deltaTime);
        animator.SetFloat(velocityZHash, velocityZ, currDamp, Time.deltaTime);
    }

    IEnumerator CrossbowGOEnabler()
    {
        yield return new WaitForSeconds(_crossbowEnablerDelay);
        crossbow.SetActive(isAgressive);
    }

    private void CheckSpeedUp()
    {
        if (moveVelocityScript.isRunning && !_isSpeededUp)
            _isSpeededUp = true;
        if (!moveVelocityScript.isRunning && _isSpeededUp)
            _isSpeededUp = false;
    }

    public void PlayFiringAnim()
    {
        animator.SetTrigger(fireTriggerHash);
    }

    public void PlayHitReactionAnim()
    {
        animator.SetTrigger(hitReactTriggerHash);
    }

    public void PlayDyingAnim()
    {
        animator.SetTrigger(dyingTriggerHash);
    }

    public void CurrentCameraMode(CameraMode mode)
    {
        cameraMode = mode;
        _isModeChanged = true;

        if (cameraMode == CameraMode.WalkMode)
        {
            isAgressive = false;
        }
        else
        {
            isAgressive = true;
        }
    }
}
