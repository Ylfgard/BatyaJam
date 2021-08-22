using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComicsPlayer : MonoBehaviour
{
    private bool slideStarted;
    private int curSlide;
    private DialogueHandler dialogHandler;
    private FMOD.Studio.EventInstance instance;
    public UnityEvent comicsEnded = new UnityEvent();
    [SerializeField] private bool playOnStart;
    [SerializeField] private GameObject[] slide;
    [SerializeField] private TextAsset[] dialogues;
    [FMODUnity.EventRef] [SerializeField] private string[] soundPath; 

    void Start() 
    {
        slideStarted = false;
        curSlide = 0;
        dialogHandler = FindObjectOfType<DialogueHandler>();
        dialogHandler.dialogueEnded.AddListener(NextSlide);
        if(playOnStart)
            UseObject();
    }

    public void UseObject()
    {
        Debug.Log("Вывел");
        if(curSlide < slide.Length)
        {
            if(!slideStarted)
            {
                slideStarted = true;
                slide[curSlide].SetActive(true);
                dialogHandler.StartDialogue(dialogues[curSlide]);
                if(soundPath[curSlide] != "")
                {
                    instance = FMODUnity.RuntimeManager.CreateInstance(soundPath[curSlide]);
                    instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform.position));
                    instance.start();
                }
            }
            else
            {
                dialogHandler.NextNode();
            }
        }
        else
        {
            comicsEnded.Invoke();
        }
    }

    void NextSlide()
    {
        curSlide++;
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        slideStarted = false;
        dialogHandler.EndDialogue();
        
    }
}
