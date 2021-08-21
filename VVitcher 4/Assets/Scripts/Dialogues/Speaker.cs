using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    private bool wasUsed = false;
    private DialogueHandler dialogHandler;
    private GameObject player; 
    private Advice advice;
    [SerializeField] private TextAsset dialogues;
    [SerializeField] private Transform adviceTransform;
    [SerializeField] private bool playSound, onlyAudio;
    [FMODUnity.EventRef] [SerializeField] private string soundPath; 

    void Start() 
    {
        player = FindObjectOfType<MovePlayer>().gameObject;
        dialogHandler = FindObjectOfType<DialogueHandler>();
        advice = FindObjectOfType<Advice>();
    }

    public void UseObject()
    {
        if(!wasUsed)
        {
            wasUsed = true;
            if(!onlyAudio)
                dialogHandler.StartDialogue(dialogues);
            if(playSound || onlyAudio)
                FMODUnity.RuntimeManager.PlayOneShotAttached(soundPath, this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            advice.ShowAdvice(adviceTransform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            if(Input.GetKeyDown(KeyCode.E) && !wasUsed)
            {
                UseObject();
                advice.HideAdvice();
            }
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            wasUsed = false;
            advice.HideAdvice();
        }
    }
}
