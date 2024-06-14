using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteLibrary : MonoBehaviour
{
    [SerializeField] private SpriteLibScriptableObject spriteLib;
    [SerializeField] private readonly Dictionary<string, Sprite> _spriteDic = new Dictionary<string, Sprite>();

    #region Properties
    public static SpriteLibrary Instance { get; private set; }

    #endregion
    
    private const int FIRST_ARRAY=0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            if (spriteLib == null) return;
            var names=spriteLib.GetStringArr();
            var sprites = spriteLib.GetSpriteArr();
            for (var index = FIRST_ARRAY; index < spriteLib.GetSize(); index++)
            {
                _spriteDic.Add(names[index], sprites[index]);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Sprite GetSprite(string categoty, string keyname)
    {
        var key = categoty + keyname;
        if (_spriteDic.TryGetValue(key, out var value))
        {
            return value;
        }
        else
        {
            Debug.LogWarning("Key not found");
            return null;
        }
    }

    public Sprite GetSprite(string key)
    {
        if (_spriteDic.TryGetValue(key, out var value))
        {
            return value;
        }
        else
        {
            Debug.LogWarning("Key not found");
            return null;
        }
    }
}
