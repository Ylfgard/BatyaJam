using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advice : MonoBehaviour
{
    private MeshRenderer adviceText;
    private Transform _transform;

    private void Start()
    {
        adviceText = gameObject.GetComponent<MeshRenderer>();
        _transform = gameObject.GetComponent<Transform>();

        HideAdvice(); 
    }

    private void Update()
    {
        FaceCamera();
    }

    public void ShowAdvice(Vector3 advicePosition)
    {
        _transform.position = advicePosition;
        adviceText.enabled = true;
    }

    public void HideAdvice()
    {
        adviceText.enabled = false;
    }

    private void FaceCamera()
    {
        if (adviceText.enabled)
            _transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
