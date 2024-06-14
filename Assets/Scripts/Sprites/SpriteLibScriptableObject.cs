using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteLibScriptableObject", menuName = "ScriptableObjects/SpriteLib")]
public class SpriteLibScriptableObject : ScriptableObject
{
    #region Fields

    [SerializeField] private SpriteElemClass[] spriteLib;

    #endregion

    #region Methods

    public int GetSize()
    {
        return spriteLib.Length;
    }

    public string[] GetStringArr()
    {
        var spritenames = spriteLib
            .Select(spritenames => spritenames.name)
            .ToArray();
        return spritenames;
    }

    public Sprite[] GetSpriteArr()
    {
        var spritenames = spriteLib
            .Select(spritenames => spritenames.sprite)
            .ToArray();
        return spritenames;
    }

    #endregion
}