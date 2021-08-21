using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    private int curDialogIndex = 0;
    private DialogueHandler dialogHandler;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private TextAsset[] dialogues;

    void Start() 
    {
        dialogHandler = FindObjectOfType<DialogueHandler>();
    }

    public void UseObject(GameObject player)
    {
        Camera.main.transform.position = cameraPosition.position;
        dialogHandler.StartDialogue(dialogues[curDialogIndex]);
    }

    public void ChangeDialogue(int newDialogIndex)
    {
        if(newDialogIndex >= 0 && newDialogIndex < dialogues.Length)
            curDialogIndex = newDialogIndex;
        else
            Debug.LogError("Выход за границы массива диалогов. Индекс: " + newDialogIndex);
    }
}
