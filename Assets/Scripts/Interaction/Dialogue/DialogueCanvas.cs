using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] private DialogueClass.Feel feel;
    [SerializeField] private Sprite pointer;
    [SerializeField] private Sprite bubble;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image bubbleImg;
    [SerializeField] private Image pointerImg;

    private const string CALM_KEY = "Calm";
    private const string MAD_KEY = "Mad";
    private const string THINK_KEY = "Think";
    private const string BUBBLE_CAT = "Dialogbubble";
    private const string POINTER_CAT = "Dialogpointer";
    
    
    private void Awake()
    {
        ChangeSpriteFeel();
        Hide();
    }

    private void ChangeSpriteFeel()
    {
        switch(feel)
        {
            case DialogueClass.Feel.Calm:
                bubble = SpriteLibrary.Instance.GetSprite(BUBBLE_CAT,CALM_KEY);
                pointer = SpriteLibrary.Instance.GetSprite(POINTER_CAT,CALM_KEY);
                break;
            case DialogueClass.Feel.Mad:
                bubble = SpriteLibrary.Instance.GetSprite(BUBBLE_CAT,MAD_KEY);
                pointer = SpriteLibrary.Instance.GetSprite(POINTER_CAT,MAD_KEY);
                break;
            case DialogueClass.Feel.Think:
                bubble = SpriteLibrary.Instance.GetSprite(BUBBLE_CAT,THINK_KEY);
                pointer = SpriteLibrary.Instance.GetSprite(POINTER_CAT,THINK_KEY);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        bubbleImg.sprite = bubble;
        pointerImg.sprite = pointer;
    }

    public void ChangeDialogueFeel(DialogueClass.Feel newfeel)
    {
        feel = newfeel;
        ChangeSpriteFeel();
    }
    
    public void Hide()
    {
        bubbleImg.enabled=false;
        text.enabled = false;
        pointerImg.enabled=false;
    }
}
