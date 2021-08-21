using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovePlayerAiming : MonoBehaviour, IMovePlayerMode
{
    [SerializeField]
    private CinemachineFreeLook aimingCam;
    [SerializeField]
    private float playerRotationSpeed = 8f;
    [SerializeField]
    private float sensitivityX = 3f;
    [SerializeField]
    private float sensitivityY = 3f;

    private Camera mainCam;
    private bool isAiming;

    private void Start()
    {
        mainCam = Camera.main;

        aimingCam.m_XAxis.m_MaxSpeed *= sensitivityX;
        aimingCam.m_YAxis.m_MaxSpeed *= sensitivityY;
    }

    private void Update()
    {
        if (isAiming)
        {
            Vector3 lookRotation = new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookRotation, Vector3.up), playerRotationSpeed);
        }
    }

    public void SetAimingBool(bool b)
    {
        isAiming = b;
    }
}
