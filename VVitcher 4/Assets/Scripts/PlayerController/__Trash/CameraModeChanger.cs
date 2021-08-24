using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraMode { WalkMode, AimMode }

public class CameraModeChanger : MonoBehaviour
{
    private const int priorityValue = 2;

    [SerializeField]
    private CinemachineFreeLook aimCamera;

    private AimTargetPosition aimTargetPositionScript;
    private AimTargetPositionOff aimTargetPositionOffScript;

    private void Start()
    {
        aimTargetPositionScript = GetComponentInChildren<AimTargetPosition>();
        aimTargetPositionOffScript = GetComponentInChildren<AimTargetPositionOff>();

        aimTargetPositionScript.enabled = false;
        aimTargetPositionOffScript.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetCameraMode(CameraMode.AimMode);
            aimCamera.Priority += priorityValue;

            aimTargetPositionScript.enabled = true;
            aimTargetPositionOffScript.enabled = false;
            //Cursor.lockState = CursorLockMode.Locked;
            //Debug.Log("On: " + aimTargetPositionScript.isActiveAndEnabled + "; Off: " + aimTargetPositionOffScript.isActiveAndEnabled);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            SetCameraMode(CameraMode.WalkMode);
            aimCamera.Priority -= priorityValue;

            aimTargetPositionScript.enabled = false;
            aimTargetPositionOffScript.enabled = true;
            //Cursor.lockState = CursorLockMode.None;
            //Debug.Log("On: " + aimTargetPositionScript.isActiveAndEnabled + "; Off: " + aimTargetPositionOffScript.isActiveAndEnabled);
        }
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
