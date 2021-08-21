using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimModeChanger : MonoBehaviour
{
    private const int priorityValue = 2;

    [SerializeField]
    private CinemachineFreeLook aimingCam;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetAimingMode(true);
            aimingCam.Priority += priorityValue;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            SetAimingMode(false);
            aimingCam.Priority -= priorityValue;
        }
    }

    private void SetAimingMode(bool b)
    {
        IMovePlayerMode[] playerMods = GetComponents<IMovePlayerMode>();
        foreach (IMovePlayerMode playerMode in playerMods)
        {
            playerMode.SetAimingBool(b);
        }
    }
}
