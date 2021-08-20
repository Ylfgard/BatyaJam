using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portret : MysticThings
{
    private Vector3 hidePosition, showPosition;
    [SerializeField] private Transform showTransform, hornsTransform;

    private void Start() 
    {
        hidePosition = hornsTransform.position;
        showPosition = showTransform.position;
    }

    public override void StartMystic()
    {
        StartCoroutine(MoveHorns(0, hidePosition, showPosition));
    }

    public override void EndMystic()
    {
        StartCoroutine(MoveHorns(0, showPosition, hidePosition));
    }

    IEnumerator MoveHorns(float progress, Vector3 oldPosition, Vector3 newPosition)
    {
        yield return new WaitForEndOfFrame();
        progress += 0.1f;
        hornsTransform.position = Vector3.Lerp(oldPosition, newPosition, progress);
        if(progress < 1)
            StartCoroutine(MoveHorns(progress, oldPosition, newPosition));
    }
}