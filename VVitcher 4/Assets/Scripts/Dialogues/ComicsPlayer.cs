using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComicsPlayer : MonoBehaviour
{
    private bool slideStarted;
    private int curSlide;
    private DialogueHandler dialogHandler;
    private FMOD.Studio.EventInstance instance;
    public UnityEvent comicsEnded = new UnityEvent();

    [SerializeField] Button continueButton;
    [SerializeField] private bool playOnStart;
    [SerializeField] private GameObject[] slide;
    [SerializeField] private TextAsset[] dialogues;
    [FMODUnity.EventRef] [SerializeField] private string[] soundPath;

    void Start() 
    {
        GamePauser.GamePause();
        slideStarted = false;
        curSlide = 0;
        dialogHandler = FindObjectOfType<DialogueHandler>();
        dialogHandler.dialogueEnded.AddListener(NextSlide);
        if(playOnStart)
            UseObject();
    }

    public void UseObject()
    {
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
            continueButton.interactable = false;
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
        GamePauser.GamePause();
    }
}
