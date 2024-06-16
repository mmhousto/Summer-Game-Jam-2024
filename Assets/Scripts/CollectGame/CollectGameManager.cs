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

    [SerializeField] private ScavengerItem[] onCollectDispatchers;

    [SerializeField] private bool[] collectedItems;

    [SerializeField] private int remainingItems;

    [SerializeField] private Transform NPC;

    private DialogueClass[] _manualDialogues;

    private Transform[] collectListElements;
    private Dialogue NPCDialogue;
    private List<FakeCollectable> CompRemoveFc;
    private List<ScavengerItem> CompRemoveSi;

    #endregion

    #region Constants

    private const int NO_REMAINING = 0;
    private const int ARRAY_START = 0;
    private const int SINGLE_DIALOGUE = 1;
    private const float RESTART_TIME = 1;

    #endregion

    #region Events

    public delegate void EndTimerAction();

    public static event EndTimerAction OnFinished;

    #endregion

    #region UnityMethods

    private void OnEnable()
    {
        TimerManager.OnTimeStart += StartGame;
        TimerManager.OnTimeOver += GameOver;
    }


    private void OnDisable()
    {
        TimerManager.OnTimeStart -= StartGame;
        TimerManager.OnTimeOver -= GameOver;
    }

    #endregion

    #region Methods

    private void StartGame()
    {
        InitializeList();
        NPCDialogue = NPC.GetComponent<Dialogue>();
        NpcDialogueRemaining();
    }

    private void InitializeList()
    {
        
        CompRemoveFc = new List<FakeCollectable>();
        CompRemoveSi = new List<ScavengerItem>();
        collectListElements = collectListParent.GetComponentsInChildren<Transform>()
            .Where(x => x != collectListParent.transform).ToArray();

        collectListElements = RearrangeArray(maxItems, collectListElements);
        collectedItems = new bool[collectListElements.Length];
        onCollectDispatchers = new ScavengerItem[collectListElements.Length];
        for (var ii = ARRAY_START; ii < collectListElements.Length; ii++)
        {
            var componentFc = collectListElements[ii].AddComponent<FakeCollectable>();
            CompRemoveFc.Add(componentFc);
            var componentSi = collectListElements[ii].AddComponent<ScavengerItem>();
            CompRemoveSi.Add(componentSi);
            onCollectDispatchers[ii] = componentSi;
            componentSi.ID = ii;
        }

        remainingItems = collectListElements.Length;
        foreach (var t in onCollectDispatchers)
        {
            t.OnObjectDisabled += OnObjectCollected;
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
        t.OnObjectDisabled -= OnObjectCollected;
        remainingItems--;
        collectedItems[id] = true;
        CheckAllObjectsAreCollected();
    }

    private string PrintRemainingItems()
    {
        var dialogue = "The remaining items are:";
        var remainingList = new List<string>();
        for (var ii = ARRAY_START; ii < collectedItems.Length; ii++)
        {
            if (collectedItems[ii]) continue;
            remainingList.Add(collectListElements[ii].name);
        }

        dialogue += string.Join(",", remainingList) + ".";

        if (remainingList.Count <= NO_REMAINING)
        {
            dialogue = "You collected all the items";
        }

        return dialogue;
    }

    private void CheckAllObjectsAreCollected()
    {
        NpcDialogueRemaining();
        if (remainingItems > NO_REMAINING) return;
        OnFinished?.Invoke();
        StartCoroutine(WaitAndRestart());
    }

    private IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(RESTART_TIME);
        foreach (var component in CompRemoveFc)
        {
            Destroy (component);
        }
        
        
        foreach (var component in CompRemoveSi)
        {
            Destroy (component);
        }
        
        foreach (var item in onCollectDispatchers)
        {
            item.gameObject.SetActive(true);
        }
    }

    private void GameOver()
    {
        //TODO: Make a game over, and a restart
        Debug.Log("Times up, you lost");
    }

    #endregion
}