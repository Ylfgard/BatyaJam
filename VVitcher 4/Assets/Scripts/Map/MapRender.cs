using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapRender : MonoBehaviour
{
    private Camera mapCamera;
    [SerializeField] private LayerMask firstFloorLayer, secondFloorLayer;
    [SerializeField] private GameObject map;
    public static UnityEvent mapOpen = new UnityEvent(), mapClose = new UnityEvent();

    private void Start()
    {
        mapCamera = gameObject.GetComponent<Camera>();    
        CloseMap();
    }

    private void Update()
    {
        if(map.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.M))
                CloseMap();
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.M))
                OpenMap();
        }
    }

    void OpenMap()
    {
        GamePauser.GamePause();
        mapOpen.Invoke();
        map.SetActive(true);
    }

    public void OnSecondFloor()
    {
        mapCamera.cullingMask = secondFloorLayer;
    }

    public void OnFirstFloor()
    {
        mapCamera.cullingMask = firstFloorLayer;
    }

    void CloseMap()
    {
        GamePauser.GameContinue();
        mapClose.Invoke();
        map.SetActive(false);
    }
}
