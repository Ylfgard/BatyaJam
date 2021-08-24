using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovePlayerWalkCam : MonoBehaviour, IPlayerCameraMode
{
    //[SerializeField]
    //private GameObject mouseTarget;
    [SerializeField]
    private CinemachineFreeLook walkCam;
    [SerializeField]
    private float playerRotationSpeed = 8f;
    [SerializeField]
    private float sensitivityX = 3f;
    [SerializeField]
    private float sensitivityY = 3f;

    private Camera mainCam;
    private MouseTargetPosition mouseTargetPositionScript;
    private MovePlayer movePlayerScript;
    private CameraMode cameraMode;
    //private bool isAiming;

    private void Start()
    {
        mainCam = Camera.main;
        //mouseTargetPositionScript = mouseTarget.GetComponent<MouseTargetPosition>();
        movePlayerScript = GetComponent<MovePlayer>();

        walkCam.m_XAxis.m_MaxSpeed *= sensitivityX;
        walkCam.m_YAxis.m_MaxSpeed *= sensitivityY;
    }

    private void FixedUpdate()
    {
        if (cameraMode == CameraMode.WalkMode)
        {
            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D))
            {
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movePlayerScript.moveDirection, Vector3.up), playerRotationSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePlayerScript.moveDirection, Vector3.up), playerRotationSpeed * Time.fixedDeltaTime);
            }
        }
    }

    public void CurrentCameraMode(CameraMode mode)
    {
        cameraMode = mode;
    }
}
