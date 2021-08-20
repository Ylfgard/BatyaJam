using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private bool oneOff;
    [SerializeField] private List<MysticThings> activateOnEnterMystics;
    [SerializeField] private List<MysticThings> diactivateOnExitMystics;

    private void Start()
    {
        player = FindObjectOfType<MovePlayer>().gameObject;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject == player)
        {
            foreach(MysticThings mystic in activateOnEnterMystics)
            {
                mystic.StartMystic();
            }    
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject == player)
        {
            foreach(MysticThings mystic in diactivateOnExitMystics)
            {
                mystic.EndMystic();
            }  

            if(oneOff) 
                gameObject.GetComponent<Collider>().enabled = false; 
        }
    }
}
