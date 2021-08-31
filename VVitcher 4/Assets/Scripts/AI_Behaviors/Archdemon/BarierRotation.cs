using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarierRotation : MonoBehaviour
{
    private Quaternion rot;

    private void Start() => rot = transform.rotation;
    private void LateUpdate() => transform.rotation = rot;
}
