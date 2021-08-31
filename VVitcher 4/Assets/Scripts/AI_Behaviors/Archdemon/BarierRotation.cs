using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarierRotation : MonoBehaviour
{
    [SerializeField] Transform archdemon;
    Quaternion rot;

    private void Start()
    {
        rot = transform.rotation;
    }

    private void LateUpdate()
    {
        //transform.rotation = Quaternion.Euler(archdemon.rotation.x, archdemon.rotation.y, archdemon.rotation.z);

        transform.rotation = rot;
    }
}
