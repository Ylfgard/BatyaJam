using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovePlayerDefault : MonoBehaviour, IMovePlayerMode
{
    [SerializeField]
    private CinemachineFreeLook defaultCam;
    [SerializeField]
    private float playerRotationSpeed = 8f;
    [SerializeField]
    private float sensitivityX = 3f;
    [SerializeField]
    private float sensitivityY = 3f;

    private MovePlayer movePlayerScript;
    private bool isAiming;

    private void Start()
    {
        movePlayerScript = GetComponent<MovePlayer>();

        defaultCam.m_XAxis.m_MaxSpeed *= sensitivityX;
        defaultCam.m_YAxis.m_MaxSpeed *= sensitivityY;
    }

    private void Update()
    {
        if (!isAiming)
        {
            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movePlayerScript.moveDirection, Vector3.up), playerRotationSpeed);
            }
        }
    }

    public void SetAimingBool(bool b)
    {
        isAiming = b;
    }
}
