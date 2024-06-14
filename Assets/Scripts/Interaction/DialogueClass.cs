using System;
 
[Serializable]
public class DialogueClass
{
    #region Fields

    public enum Feel {Calm,Mad,Think};
    
    public string dialogue;
    public Feel feeling;
    public bool isPlayer;

    #endregion

    #region Constructor

    public DialogueClass(string dialogue, Feel feeling, bool isPlayer)
    {
        this.dialogue = dialogue;
        this.feeling = feeling;
        this.isPlayer = isPlayer;
    }

    #endregion

}