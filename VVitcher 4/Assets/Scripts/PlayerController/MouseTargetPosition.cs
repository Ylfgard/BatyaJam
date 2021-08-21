using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTargetPosition : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private LayerMask mouseTargetLayer;

    private Camera mainCam;

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

        if (Physics.Raycast(ray, out hit, float.MaxValue, mouseTargetLayer))
        {
            transform.position = hit.point;
        }
    }
}
