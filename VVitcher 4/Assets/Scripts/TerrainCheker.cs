using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCheker : MonoBehaviour
{
    private FootstepPlayer footstepPlayer;

    private void Start()
    {
        footstepPlayer = FindObjectOfType<FootstepPlayer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if(other.gameObject.layer == footstepPlayer.gameObject.layer)
            footstepPlayer.surface = 0;
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.layer == footstepPlayer.gameObject.layer)
            footstepPlayer.surface = 1;
    }
}
