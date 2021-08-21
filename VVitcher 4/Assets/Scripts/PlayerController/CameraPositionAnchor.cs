using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionAnchor : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        Vector3 offsetPos = new Vector3(mainCam.transform.position.x, player.transform.position.y, mainCam.transform.position.z);
        transform.position = offsetPos;
        transform.rotation = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y, mainCam.transform.rotation.eulerAngles.z);
    }
}
