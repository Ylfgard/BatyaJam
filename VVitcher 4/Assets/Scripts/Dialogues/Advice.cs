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

    public void ShowAdvice(Vector3 advicePosition)
    {
        _transform.position = advicePosition;
        adviceText.enabled = true;
    }

    public void HideAdvice()
    {
        adviceText.enabled = false;
    }
}
