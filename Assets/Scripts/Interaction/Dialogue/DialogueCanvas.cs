using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Debug = UnityEngine.Debug;

public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] private DialogueClass.Feel feel;
    [SerializeField] private Sprite pointer;
    [SerializeField] private Sprite bubble;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image bubbleImg;
    [SerializeField] private Image pointerImg;
    [SerializeField] private bool followCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private bool resetDialogue;

    private readonly Vector3 MIRROR_FLIP = new Vector3(0, 180, 0);

    private const string CALM_KEY = "Calm";
    private const string MAD_KEY = "Mad";
    private const string THINK_KEY = "Think";
    private const string BUBBLE_CAT = "Dialogbubble";
    private const string POINTER_CAT = "Dialogpointer";


    private void Start()
    {
        ChangeSpriteFeel();
        HideDialogue();
    }

    private void LateUpdate()
    {
        FollowObject();
    }

    private void FollowObject()
    {
        if (!followCamera) return;
        var camPos = mainCamera.transform.position;
        var position = transform.position;
        
        var targetPostition = new Vector3(camPos.x,position.y, camPos.z);
        transform.LookAt(targetPostition);
        transform.Rotate(MIRROR_FLIP);
        
        var dist = Vector3.Distance(camPos, position);
        if(dist<15)return;
        EndDialogue();
    }

    public void setCameraFollow()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            followCamera = true;
        }
    }

    private void ChangeSpriteFeel()
    {
        switch (feel)
        {
            case DialogueClass.Feel.Calm:
                bubble = SpriteLibrary.Instance.GetSprite(BUBBLE_CAT, CALM_KEY);
                pointer = SpriteLibrary.Instance.GetSprite(POINTER_CAT, CALM_KEY);
                break;
            case DialogueClass.Feel.Mad:
                bubble = SpriteLibrary.Instance.GetSprite(BUBBLE_CAT, MAD_KEY);
                pointer = SpriteLibrary.Instance.GetSprite(POINTER_CAT, MAD_KEY);
                break;
            case DialogueClass.Feel.Think:
                bubble = SpriteLibrary.Instance.GetSprite(BUBBLE_CAT, THINK_KEY);
                pointer = SpriteLibrary.Instance.GetSprite(POINTER_CAT, THINK_KEY);
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

    public void HideDialogue()
    {
        bubbleImg.enabled = false;
        dialogueText.enabled = false;
        pointerImg.enabled = false;
    }

    public void ShowDialogue()
    {
        bubbleImg.enabled = true;
        dialogueText.enabled = true;
        pointerImg.enabled = true;
    }

    public void EndDialogue()
    {
        HideDialogue();
        followCamera = false;
        dialogueText.text = "";
        resetDialogue = true;
    }

    public bool GetResetDialogue()
    {
        return resetDialogue;
    }

    public void DisplayDialogue(DialogueClass.Feel newfeel, string dialogue)
    {
        ChangeDialogueFeel(newfeel);
        dialogueText.text = dialogue;
    }
}