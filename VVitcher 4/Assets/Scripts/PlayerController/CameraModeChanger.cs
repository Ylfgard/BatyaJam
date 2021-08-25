using Cinemachine;
using UnityEngine;

public enum CameraMode { WalkMode, AimMode }

public class CameraModeChanger : MonoBehaviour
{
    private const int priorityAimValueDefault = 8;
    private const int priorityAimValueHigher = 10;


    [SerializeField]
    private CinemachineFreeLook aimCamera;

    private AimTargetPosition aimTargetPositionScript;
    private AimTargetPositionOff aimTargetPositionOffScript;
    private UiManager uiManagerScript;


    private void Start()
    {
        aimTargetPositionScript = GetComponentInChildren<AimTargetPosition>();
        aimTargetPositionOffScript = GetComponentInChildren<AimTargetPositionOff>();
        uiManagerScript = FindObjectOfType<UiManager>().gameObject.GetComponent<UiManager>();


        aimTargetPositionScript.enabled = false;
        aimTargetPositionOffScript.enabled = false;
    }

    public bool CanChangeMode()
    {
        return !(GamePauser.isGamePaused || uiManagerScript.IsUsingUI());
    }

    private void Update()
    {
        if (!CanChangeMode()) return;

        if (Input.GetMouseButtonDown(1))
        {
            SetPlayerAimMode();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            SetPlayerWalkMode();
        }
    }

    public void SetPlayerWalkMode()
    {
        SetCameraMode(CameraMode.WalkMode);
        aimCamera.Priority = priorityAimValueDefault;

        aimTargetPositionScript.enabled = false;
        aimTargetPositionOffScript.enabled = true;
    }

    public void SetPlayerAimMode()
    {
        SetCameraMode(CameraMode.AimMode);
        aimCamera.Priority = priorityAimValueHigher;

        aimTargetPositionScript.enabled = true;
        aimTargetPositionOffScript.enabled = false;
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
