using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovePlayerDefault : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseTarget;
    [SerializeField]
    private float playerRotationSpeed = 8f;

    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(LookDirection(), Vector3.up), playerRotationSpeed);
    }

    public Vector3 LookDirection()
    {
        Vector3 lookAt = new Vector3(mouseTarget.transform.position.x, transform.position.y, mouseTarget.transform.position.z);
        Vector3 direction = lookAt - transform.position;
        return direction;
    }
}
