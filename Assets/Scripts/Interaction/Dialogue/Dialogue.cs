using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Interactable
{
    #region Fields

    [SerializeField] private new string name;
    [SerializeField] private int dialogueIdx;
    [SerializeField] private DialogueClass[] dialogues;
    [SerializeField] private DialogueCanvas dialogueCanvas;
    #endregion

    #region Properties

    public string Name
    {
        get => name;
        set => name = value;
    }

    #endregion

    #region Constants

    private const int FIRST_DIALOGUE = 0;

    #endregion

    #region Unity Methods

    private void Start()
    {
        canInteract = true;
        dialogueCanvas.HideDialogue();
    }

    #endregion

    #region Methods

    public override void Interact(Transform interactedTarget)
    {
        if (dialogueCanvas.GetResetDialogue())
        {
            canInteract = true;
            dialogueIdx = FIRST_DIALOGUE;
        }
        if (!canInteract) return;
        canInteract = false;
        if (dialogueIdx == FIRST_DIALOGUE)
        {
            StartDialogue(dialogues,interactedTarget);
        }
        Debug.Log($"BugHunt dialogueIdx {dialogueIdx} of {dialogues.Length}");
        while (dialogueIdx<dialogues.Length)
        {
            // if (!Input.GetKeyDown(KeyCode.E)) continue;
            dialogueIdx++;
            Debug.Log($"BugHunt dialogueIdx {dialogueIdx}");
            dialogueCanvas.DisplayDialogue(dialogues[dialogueIdx].feeling,dialogues[dialogueIdx].dialogue);
        }
        Debug.Log($"BugHunt dialogueIdx {dialogueIdx} of {dialogues.Length} leaving while");
        dialogueCanvas.EndDialogue();
    }
    
    private void StartDialogue(IReadOnlyList<DialogueClass> dialogues,Transform interactedTaget)
    {
        dialogueCanvas.setCameraFollow();
        dialogueCanvas.DisplayDialogue(dialogues[dialogueIdx].feeling,dialogues[dialogueIdx].dialogue);
        dialogueCanvas.ShowDialogue();
    }
    #endregion
}