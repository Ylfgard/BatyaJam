using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour
{
    private Camera mapCamera;
    private Transform _transform;
    private bool canMove = false;
    private float xRightBorder, xLeftBorder, zUpperBorder, zLowerBorder;
    private float defaultMapZoom, maxMapZoom;
    [SerializeField] private float speed, scrollSensivity;
    

    void Start()
    {
        mapCamera = gameObject.GetComponent<Camera>();
        defaultMapZoom = mapCamera.orthographicSize;
        maxMapZoom = 10;

        _transform = gameObject.GetComponent<Transform>();

        Vector3 mapMiddle = _transform.position;
        zUpperBorder = mapMiddle.z + mapCamera.orthographicSize;
        zLowerBorder = mapMiddle.z - mapCamera.orthographicSize;
        xRightBorder = mapMiddle.x + (mapCamera.orthographicSize / mapCamera.scaledPixelHeight * mapCamera.scaledPixelWidth);
        xLeftBorder = mapMiddle.x - (mapCamera.orthographicSize / mapCamera.scaledPixelHeight * mapCamera.scaledPixelWidth);

        canMove = false;
        MapRender.mapOpen.AddListener(MapOpen);
        MapRender.mapClose.AddListener(MapClose);
        mapCamera.orthographicSize -= 0.01f;
    }

    void Update()
    {
        if(canMove)
        {
            Vector3 move = Vector3.zero;
            Vector3 mapCameraPosition = _transform.position;
            float zArgument = mapCamera.orthographicSize;
            float xArgument = mapCamera.orthographicSize / mapCamera.scaledPixelHeight * mapCamera.scaledPixelWidth;

            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");
            float mw = -Input.GetAxisRaw("Mouse ScrollWheel") * Time.unscaledDeltaTime * scrollSensivity;
            
            if(mw != 0 && mapCamera.orthographicSize+mw < defaultMapZoom && mapCamera.orthographicSize+mw > maxMapZoom)
                mapCamera.orthographicSize += mw; 

            if(mapCameraPosition.x + xArgument >= xRightBorder)
            {
                mapCameraPosition.x = xRightBorder - xArgument;
                _transform.position = mapCameraPosition;
                if(move.x > 0) move.x = 0;
            } 
            else if(mapCameraPosition.x - xArgument <= xLeftBorder)
            {
                mapCameraPosition.x = xLeftBorder + xArgument;
                _transform.position = mapCameraPosition;
                if(move.x < 0) move.x = 0;
            }
                
            if(mapCameraPosition.z + zArgument >= zUpperBorder)
            {
                mapCameraPosition.z = zUpperBorder - zArgument;
                _transform.position = mapCameraPosition;
                if(move.y > 0) move.y = 0;
            }
            else if(mapCameraPosition.z - zArgument <= zLowerBorder)
            {
                mapCameraPosition.z = zLowerBorder + zArgument;
                _transform.position = mapCameraPosition;
                if(move.y < 0) move.y = 0;
            }

            if(move != Vector3.zero)
                _transform.Translate(move * Time.unscaledDeltaTime * speed);
        }
    }

    void MapOpen() => canMove = true;

    void MapClose() => canMove = false;
}
