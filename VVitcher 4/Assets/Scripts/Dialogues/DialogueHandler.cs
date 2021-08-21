using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueHandler : MonoBehaviour
{
    private int curNodeIndex = 0;
    private float curStepTimeDelay = 0;
    private DialogueViewer curDialogue;
    private Text commentText;
    private UnityEvent nextStep = new UnityEvent(), writingFinished = new UnityEvent();
    [SerializeField] private GameObject commentFrame, dialogueFrame, dialogueCloseButton;
    [SerializeField] private Text nodeText;
    [SerializeField] private float commentHideTime;

    private void Start() 
    {
        gameObject.GetComponent<DialogueHandler>().
        writingFinished.AddListener(NextStepDelayCaller);
        commentText = commentFrame.GetComponentInChildren<Text>();
        HideComment();
        EndDialogue();
    }

    public void ShowComment(string comment)
    {
        commentText.text = "";
        commentFrame.SetActive(true);
        curStepTimeDelay = commentHideTime;
        StartCoroutine(TextByLetters(commentText, comment, 0));
        nextStep.AddListener(HideComment);
    }

    public void HideComment()
    {
        nextStep.RemoveListener(HideComment);
        commentFrame.SetActive(false);
    }

    public void StartDialogue(TextAsset dialogue)
    {
        GamePauser.GamePause();
        nodeText.text = "";
        dialogueCloseButton.SetActive(false);
        curDialogue = DialogueViewer.Load(dialogue);
        dialogueFrame.SetActive(true);
        curNodeIndex = 0;
        nextStep.AddListener(NextNode);
        NextNode();
    }

    public void NextNode()
    {
        if(curNodeIndex < curDialogue.nodes.Length)
        {
            nodeText.text += "\n\t";
            curStepTimeDelay = curDialogue.nodes[curNodeIndex].duration; 
            StartCoroutine(TextByLetters(nodeText, curDialogue.nodes[curNodeIndex].text, 0));
            curNodeIndex++;
        }
        else
        {
            dialogueCloseButton.SetActive(true);
        }
    }

    public void EndDialogue()
    {
        nextStep.RemoveListener(NextNode);
        dialogueFrame.SetActive(false);
        GamePauser.GameContinue();
    }

    void NextStepDelayCaller()
    {
        StartCoroutine(NextStepDelay());
    }

    IEnumerator TextByLetters(Text textObj, string text, int letterIndex)
    {
        yield return new WaitForEndOfFrame();
        textObj.text += text[letterIndex];
        letterIndex++;
        if(letterIndex < text.Length)
            StartCoroutine(TextByLetters(textObj, text, letterIndex));
        else
            writingFinished?.Invoke();
    }

    IEnumerator NextStepDelay()
    {
        yield return new WaitForSecondsRealtime(curStepTimeDelay);
        nextStep?.Invoke();
    }
}
