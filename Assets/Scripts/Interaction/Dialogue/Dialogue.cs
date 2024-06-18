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
    [SerializeField] private bool dialogueStarted;

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
        dialogueStarted = false;
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
            dialogueStarted = false;
            dialogueIdx = FIRST_DIALOGUE;
        }

        if (!canInteract) return;
        canInteract = false;
        if (!dialogueStarted)
        {
            dialogueStarted = true;
            InitializeDialogue();
            ShowDialogue();
        }
        else
        {
            ShowDialogue();
        }
    }

    private void InitializeDialogue()
    {
        dialogueCanvas.SetCameraFollow();
        dialogueCanvas.ShowDialogue();
        canInteract = true;
    }

    private void ShowDialogue()
    {
        if (dialogueIdx >= dialogues.Length)
        {
            EndDialogue();
            return;
        }

        dialogueCanvas.DisplayDialogue(dialogues[dialogueIdx].feeling, dialogues[dialogueIdx].dialogue,
            dialogues[dialogueIdx].dialogueSource);
        dialogueIdx++;
        canInteract = true;
    }

    private void EndDialogue()
    {
        dialogueCanvas.EndDialogue();
        dialogueStarted = false;
        dialogueIdx = FIRST_DIALOGUE;
        canInteract = true;
    }

    public void SetManualDialogue(DialogueClass[] newDialogues)
    {
        dialogues = newDialogues;
    }

    #endregion
}