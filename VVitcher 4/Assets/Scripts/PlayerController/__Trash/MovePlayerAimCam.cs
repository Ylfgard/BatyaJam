using Cinemachine;
using UnityEngine;

public class MovePlayerAimCam : MonoBehaviour, IPlayerCameraMode
{
    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private Transform aimTargetOffset;
    [SerializeField]
    private CinemachineFreeLook aimCam;
    [SerializeField]
    private float playerRotationSpeed = 8f;
    [SerializeField]
    private float sensitivityX = 3f;
    [SerializeField]
    private float sensitivityY = 3f;

    private Camera mainCam;
    private Transform camPosAnchor;
    private CameraMode cameraMode;

    private const float distanceOfRay = 300f;

    private void Start()
    {
        mainCam = Camera.main;
        camPosAnchor = GetComponentInChildren<CameraPositionAnchor>().gameObject.transform;

        aimCam.m_XAxis.m_MaxSpeed *= sensitivityX;
        aimCam.m_YAxis.m_MaxSpeed *= sensitivityY;
    }

    private void FixedUpdate()
    {
        if (!GamePauser.isGamePaused && cameraMode == CameraMode.AimMode)
        {
            Vector3 lookRotation = new Vector3(camPosAnchor.forward.x, 0, camPosAnchor.forward.z);
            Quaternion rot = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookRotation, Vector3.up), playerRotationSpeed);
            transform.rotation = rot;

            aimTarget.position = SetAimTargetPosition(0.5f);
            aimTargetOffset.position = SetAimTargetPosition(0);
        }
    }

    private Vector3 SetAimTargetPosition(float screenHeightDivisor)
    {
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * screenHeightDivisor, mainCam.nearClipPlane));
        Vector3 v = ray.origin + ray.direction * distanceOfRay;
        return v;
    }

    public void CurrentCameraMode(CameraMode mode)
    {
        cameraMode = mode;
    }
}
