using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueHandler : MonoBehaviour
{
    private int curNodeIndex = 0;
    private DialogueViewer curDialogue;
    private Text commentText;
    private UnityEvent nextStep = new UnityEvent();
    [SerializeField] private GameObject commentFrame, dialogueFrame;
    [SerializeField] private Text speakerName, nodeText;
    [SerializeField] private float commentHideTime;

    private void Start() 
    {
        commentText = commentFrame.GetComponentInChildren<Text>();
        HideComment();
        //EndDialogue();
    }

    public void ShowComment(string comment)
    {
        commentText.text = "";
        commentFrame.SetActive(true);
        StartCoroutine(TextByLetters(commentText, comment, 0));
        nextStep.AddListener(HideComment);
        StartCoroutine(NextStepDelay(commentHideTime));
    }

    public void HideComment()
    {
        nextStep.RemoveListener(HideComment);
        commentFrame.SetActive(false);
    }

    public void StartDialogue(TextAsset dialogue)
    {
        curDialogue = DialogueViewer.Load(dialogue);
        dialogueFrame.SetActive(true);
        ToNode(0);
    }

    public void ToNode(int newNodeIndex)
    {
        if(newNodeIndex >= 0 && newNodeIndex < curDialogue.nodes.Length)
        {
            curNodeIndex = newNodeIndex;
            speakerName.text = curDialogue.nodes[curNodeIndex].speakerName;
            nodeText.text = curDialogue.nodes[curNodeIndex].text;
        }
        else
        {
            Debug.LogError("Выход за границы массива абзацев. Индекс: " + newNodeIndex);
        }    
    }

    public void EndDialogue()
    {
        dialogueFrame.SetActive(false);
    }

    IEnumerator TextByLetters(Text textObj, string text, int letterIndex)
    {
        yield return new WaitForEndOfFrame();
        textObj.text += text[letterIndex];
        letterIndex++;
        if(letterIndex < text.Length)
            StartCoroutine(TextByLetters(textObj, text, letterIndex));
    }

    IEnumerator NextStepDelay(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        nextStep.Invoke();
    }
}
