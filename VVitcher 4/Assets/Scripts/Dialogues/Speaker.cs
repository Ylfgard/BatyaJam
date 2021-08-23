using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    private bool wasUsed = false;
    private DialogueHandler dialogHandler;
    private GameObject player; 
    private Advice advice;
    private FMOD.Studio.EventInstance instance;
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
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance = FMODUnity.RuntimeManager.CreateInstance(soundPath);
                instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                instance.start();
            }
        }
    }

    private void OnDestroy()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
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
            if(Input.GetKeyDown(KeyCode.F) && !wasUsed)
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
