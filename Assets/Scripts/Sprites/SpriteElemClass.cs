using System;
using UnityEngine;

[Serializable]
public class SpriteElemClass
{
    #region Fields
    
    public string name;
    public Sprite sprite;

    #endregion

    #region Constructor

    public SpriteElemClass(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }

    #endregion

}