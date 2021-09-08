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
    private CinemachineCameraOffset acOffset;
    private CameraMode cameraMode;

    private PlayerMain pMain;

    private const float distanceOfRay = 300f;

    private void Start()
    {
        mainCam = Camera.main;
        acOffset = aimCam.gameObject.GetComponent<CinemachineCameraOffset>();
        camPosAnchor = GetComponentInChildren<CameraPositionAnchor>().gameObject.transform;
        pMain = GetComponent<PlayerMain>();

        aimCam.m_XAxis.m_MaxSpeed *= sensitivityX;
        aimCam.m_YAxis.m_MaxSpeed *= sensitivityY;
    }

    private void Update()
    {
        if (!GamePauser.isGamePaused && cameraMode == CameraMode.AimMode)
        {
            aimTarget.position = SetAimTargetPosition(0.5f);
            aimTargetOffset.position = SetAimTargetPosition(0);

            Vector3 lookRotation = new Vector3(camPosAnchor.forward.x, 0, camPosAnchor.forward.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookRotation, Vector3.up), playerRotationSpeed);
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

    public void AimCamFov(float fov)
    {
        //Vector3 offsetVector = acOffset.m_Offset;
        //offsetVector = new Vector3(offsetVector.x, offsetVector.y, offset);
        aimCam.m_Lens.FieldOfView = fov;
    }
}
