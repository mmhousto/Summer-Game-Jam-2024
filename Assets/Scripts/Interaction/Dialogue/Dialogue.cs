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
            StartDialogue();
        }
    }

    private void ContinueDialogue()
    {
        if (dialogueIdx >= dialogues.Length)
        {
            EndDialogue();
            return;
        }

        dialogueCanvas.DisplayDialogue(dialogues[dialogueIdx].feeling, dialogues[dialogueIdx].dialogue, dialogues[dialogueIdx].dialogueSource);
        dialogueIdx++;
        StartCoroutine(waitForNextDialogue());
    }

    private void EndDialogue()
    {
        dialogueCanvas.EndDialogue();
        dialogueIdx = FIRST_DIALOGUE;
        StartCoroutine(TalkCooldown());
    }

    IEnumerator TalkCooldown()
    {
        yield return new WaitForSeconds(2f);
        canInteract = true;
    }

    IEnumerator waitForNextDialogue()
    {
        yield return new WaitForSeconds(0.2f);
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }

        ContinueDialogue();
    }

    private void StartDialogue()
    {
        dialogueCanvas.SetCameraFollow();
        dialogueCanvas.ShowDialogue();
        ContinueDialogue();
    }

    #endregion
}