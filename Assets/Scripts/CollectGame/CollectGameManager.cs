using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectGameManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private int maxItems;

    [SerializeField] private Transform collectListParent;

    [SerializeField] private ScavengerItem[] onDestroyDispatchers;

    [SerializeField] private bool[] deletedItems;

    [SerializeField] private int remainingItems;

    [SerializeField] private Transform NPC;

    private DialogueClass[] _manualDialogues;

    private Transform[] collectListElements;
    private Dialogue NPCDialogue;

    #endregion

    #region Constants

    private const int NO_REMAINING = 0;
    private const int ARRAY_START = 0;
    private const int SINGLE_DIALOGUE = 1;

    #endregion

    #region UnityMethods

    private void Start()
    {
        InitializeList();
        NPCDialogue = NPC.GetComponent<Dialogue>();
        NpcDialogueRemaining();
    }

    #endregion

    #region Methods

    private void InitializeList()
    {
        collectListElements = collectListParent.GetComponentsInChildren<Transform>()
            .Where(x => x != collectListParent.transform).ToArray();

        collectListElements = RearrangeArray(maxItems, collectListElements);
        deletedItems = new bool[collectListElements.Length];
        onDestroyDispatchers = new ScavengerItem[collectListElements.Length];
        for (var ii = ARRAY_START; ii < collectListElements.Length; ii++)
        {
            collectListElements[ii].AddComponent<Collectable>();
            var myC = collectListElements[ii].AddComponent<ScavengerItem>();
            onDestroyDispatchers[ii] = myC;
            myC.ID = ii;
        }

        remainingItems = collectListElements.Length;
        foreach (var t in onDestroyDispatchers)
        {
            t.OnObjectDestroyed += OnObjectCollected;
        }
    }

    private void NpcDialogueRemaining()
    {
        if (NPC == null) return;
        var singleDialogue = new DialogueClass(PrintRemainingItems(), DialogueClass.Feel.Calm, NPC.transform);
        var newDialogues = new DialogueClass[SINGLE_DIALOGUE];
        newDialogues[ARRAY_START] = singleDialogue;
        NPCDialogue.SetManualDialogue(newDialogues);
    }

    private Transform[] RearrangeArray(int size, Transform[] collectList)
    {
        if (size < ARRAY_START || size >= this.collectListElements.Length) return collectList;
        for (var ii = ARRAY_START; ii < collectList.Length; ii++)
        {
            var tmp = collectList[ii];
            var r = Random.Range(ii, collectList.Length);
            collectList[ii] = collectList[r];
            collectList[r] = tmp;
        }

        return collectList.Where((e, i) => i < size).ToArray();
    }

    private void OnObjectCollected(int id, ScavengerItem t)
    {
        t.OnObjectDestroyed -= OnObjectCollected;
        remainingItems--;
        deletedItems[id] = true;
        CheckAllObjectsAreCollected();
    }

    private string PrintRemainingItems()
    {
        var dialogue = "The remaining items are:";
        var remainingList = new List<string>();
        for (var ii = ARRAY_START; ii < deletedItems.Length; ii++)
        {
            if (deletedItems[ii]) continue;
            remainingList.Add(collectListElements[ii].name);
        }
        
        dialogue += string.Join( ",", remainingList )+".";
        
        if (remainingList.Count <= NO_REMAINING)
        {
            dialogue = "You collected all the items";
        }

        return dialogue;
    }

    private void CheckAllObjectsAreCollected()
    {
        NpcDialogueRemaining();
        if (remainingItems <= NO_REMAINING)
        {
            //TODO: End the minigame
            Debug.Log("All items collected");
        }
    }

    #endregion
}