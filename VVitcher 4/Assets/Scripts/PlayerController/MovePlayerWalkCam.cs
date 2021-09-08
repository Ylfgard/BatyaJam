using Cinemachine;
using UnityEngine;

public class MovePlayerWalkCam : MonoBehaviour, IPlayerCameraMode
{
    [SerializeField]
    private CinemachineFreeLook walkCam;
    [SerializeField]
    private float playerRotationSpeed = 8f;
    [SerializeField]
    private float sensitivityX = 3f;
    [SerializeField]
    private float sensitivityY = 3f;

    private CameraMode cameraMode;
    private PlayerMain playerMainScript;
    private MovePlayer movePlayerScript;

    private void Start()
    {
        playerMainScript = GetComponent<PlayerMain>();
        movePlayerScript = GetComponent<MovePlayer>();

        walkCam.m_XAxis.m_MaxSpeed *= sensitivityX;
        walkCam.m_YAxis.m_MaxSpeed *= sensitivityY;
    }

    private void Update()
    {
        if (!playerMainScript.isDead && !GamePauser.isGamePaused && (cameraMode == CameraMode.WalkMode || cameraMode == CameraMode.RunAimMode))
        {
            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D))
            {
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movePlayerScript.moveDirection, Vector3.up), playerRotationSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePlayerScript.moveDirection, Vector3.up), playerRotationSpeed * Time.deltaTime);
            }
        }
    }

    public void CurrentCameraMode(CameraMode mode)
    {
        cameraMode = mode;
    }
}
