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
        dialogueCanvas.enabled = false;
    }

    #endregion

    #region Methods

    public override void Interact()
    {
        if (!canInteract) return;
        canInteract = false;
        StartCoroutine(TalkPause());
       
        switch (dialogues[dialogueIdx].feeling)
        {
            case DialogueClass.Feel.Calm:
                Debug.Log($"{dialogueIdx}->{name}: {dialogues[dialogueIdx].dialogue}");
                break;
            case DialogueClass.Feel.Mad:
                Debug.Log($"{dialogueIdx}->{name}: !!!{dialogues[dialogueIdx].dialogue}!!!");
                break;
            case DialogueClass.Feel.Think:
                Debug.Log($"{dialogueIdx}->{name}: ¿¿¿{dialogues[dialogueIdx].dialogue}???");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        dialogueIdx++;
        if (dialogueIdx >= dialogues.Length)
        {
            dialogueIdx = FIRST_DIALOGUE;
        }
    }

    private IEnumerator TalkPause()
    {
        yield return new WaitForSeconds(0.5f);
        canInteract = true;
    }

    #endregion
}