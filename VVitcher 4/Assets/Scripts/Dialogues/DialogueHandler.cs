using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueHandler : MonoBehaviour
{
    private int curNodeIndex = 0;
    private DialogueViewer curDialogue;
    private UnityEvent nextStep = new UnityEvent();
    [SerializeField] private GameObject dialogueFrame;
    [SerializeField] private Text commentText, speakerName, nodeText;
    [SerializeField] private float commentHideTime;

    private void Start() 
    {
        HideComment();
        //EndDialogue();
    }

    public void ShowComment(string comment)
    {
        commentText.text = "";
        commentText.enabled = true;
        foreach(char letter in comment)
            StartCoroutine(TextByLetters(commentText, letter));
        nextStep.AddListener(HideComment);
        StartCoroutine(NextStepDelay(commentHideTime));
    }

    public void HideComment()
    {
        nextStep.RemoveListener(HideComment);
        commentText.enabled = false;
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

    IEnumerator TextByLetters(Text textObj, char letter)
    {
        yield return new WaitForEndOfFrame();
        textObj.text += letter;
    }

    IEnumerator NextStepDelay(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        nextStep.Invoke();
    }
}
