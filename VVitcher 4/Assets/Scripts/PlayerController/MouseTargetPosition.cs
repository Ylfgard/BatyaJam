using UnityEngine;

public class MouseTargetPosition : MonoBehaviour
{
    [SerializeField]
    private LayerMask mouseTargetLayer;

    private Camera mainCam;
    private const float lengthOfRay = 300f;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        SetMouseTargetPosition();
    }

    public void SetMouseTargetPosition()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, lengthOfRay, mouseTargetLayer))
        {
            transform.position = hit.point;
            Gizmos.DrawRay(mainCam.transform.position, mainCam.transform.forward);
        }
    }
}
