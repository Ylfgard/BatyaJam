using Cinemachine;
using System.Collections;
using UnityEngine;

public enum CameraMode { WalkMode, AimMode }

public class CameraModeChanger : MonoBehaviour
{
    private const int priorityAimValueDefault = 8;
    private const int priorityAimValueHigher = 10;

    [SerializeField]
    private CinemachineFreeLook aimCamera;
    [SerializeField]
    private float crossbairDelay = 0.3f;

    private AimTargetPosition aimTargetPositionScript;
    private AimTargetPositionOff aimTargetPositionOffScript;
    private UiManager uiManagerScript;
    private PlayerMain playerMainScript;

    public delegate void CrossbairEnablerDelegate(bool canEnable);
    public CrossbairEnablerDelegate crossbairEnable;

    public bool isAiming { get; set; }

    private void Start()
    {
        aimTargetPositionScript = GetComponentInChildren<AimTargetPosition>();
        aimTargetPositionOffScript = GetComponentInChildren<AimTargetPositionOff>();
        uiManagerScript = FindObjectOfType<UiManager>().gameObject.GetComponent<UiManager>();
        playerMainScript = GetComponent<PlayerMain>();

        aimTargetPositionScript.enabled = false;
        aimTargetPositionOffScript.enabled = false;
    }

    public bool CanChangeMode()
    {
        return !(GamePauser.isGamePaused || uiManagerScript.IsUsingUI() || playerMainScript.isDead);
    }

    private void Update()
    {
        if (!CanChangeMode()) return;

        if (Input.GetMouseButtonDown(1))
        {
            isAiming = !isAiming;
            ChangeMode();
        }
        
    }

    private void ChangeMode()
    {
        if (isAiming)
            SetPlayerAimMode();
        else
            SetPlayerWalkMode();
    }

    public void SetPlayerWalkMode()
    {
        SetCameraMode(CameraMode.WalkMode);
        aimCamera.Priority = priorityAimValueDefault;

        aimTargetPositionScript.enabled = false;
        aimTargetPositionOffScript.enabled = true;

        crossbairEnable(false);
        StopAllCoroutines();
    }

    public void SetPlayerAimMode()
    {
        SetCameraMode(CameraMode.AimMode);
        aimCamera.Priority = priorityAimValueHigher;

        aimTargetPositionScript.enabled = true;
        aimTargetPositionOffScript.enabled = false;

        StartCoroutine(PrepareCrossbair());
    }

    IEnumerator PrepareCrossbair()
    {
        yield return new WaitForSeconds(crossbairDelay);
        crossbairEnable(true);
    }

    private void SetCameraMode(CameraMode mode)
    {
        IPlayerCameraMode[] playerCameraModes = GetComponents<IPlayerCameraMode>();
        foreach (IPlayerCameraMode pCamMode in playerCameraModes)
        {
            pCamMode.CurrentCameraMode(mode);
        }
    }
}
