using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComment : MysticThings
{
    private DialogueHandler dialogueHandler;
    [SerializeField] private string commentText;

    private void Start()
    {
        dialogueHandler = FindObjectOfType<DialogueHandler>();
    }

    public override void StartMystic()
    {
        dialogueHandler.ShowComment(commentText);
    }
}
