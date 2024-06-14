using System;
 
[Serializable]
public class DialogueClass
{
    #region Fields

    public enum Feel {Calm,Mad,Think};
    
    public string dialogue;
    public Feel feeling;

    #endregion

    #region Constructor

    public DialogueClass(string dialogue, Feel feeling)
    {
        this.dialogue = dialogue;
        this.feeling = feeling;
    }

    #endregion

}