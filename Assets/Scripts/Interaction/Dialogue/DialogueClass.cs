using System;
using UnityEngine;

[Serializable]
public class DialogueClass
{
    #region Fields

    public enum Feel {Calm,Mad,Think};
    
    public string dialogue;
    public Feel feeling;
    public Transform dialogueSource;

    #endregion

    #region Constructor

    public DialogueClass(string dialogue, Feel feeling, Transform dialogueSource)
    {
        this.dialogue = dialogue;
        this.feeling = feeling;
        this.dialogueSource = dialogueSource;
    }

    #endregion

}