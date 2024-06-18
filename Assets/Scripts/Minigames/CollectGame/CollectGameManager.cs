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

    [SerializeField] private Transform npc;

    [SerializeField] private bool gameIsActive;

    [SerializeField] private bool wonOnce;

    [SerializeField] private string winningText;

    private DialogueClass[] _manualDialogues;

    private Transform[] _collectListElements;
    private Dialogue _npcDialogue;
    private List<FakeCollectable> _compRemoveFc;
    private List<ScavengerItem> _compRemoveSi;

    #endregion

    #region Constants

    private const int NO_REMAINING = 0;
    private const int ARRAY_START = 0;
    private const float RESTART_TIME = 2;

    #endregion

    #region Events

    public delegate void ChangeGameStatusAction();

    public static event ChangeGameStatusAction OnFinished;
    public static event ChangeGameStatusAction OnStarted;

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
        if (gameIsActive) return;
        gameIsActive = true;
        InitializeList();
        _npcDialogue = npc.GetComponent<Dialogue>();
        OnStarted?.Invoke();
        NpcDialogueRemaining();
    }

    private void InitializeList()
    {
        _compRemoveFc = new List<FakeCollectable>();
        _compRemoveSi = new List<ScavengerItem>();
        _collectListElements = collectListParent.GetComponentsInChildren<Transform>()
            .Where(x => x != collectListParent.transform).ToArray();

        _collectListElements = RearrangeArray(maxItems, _collectListElements);
        collectedItems = new bool[_collectListElements.Length];
        onCollectDispatchers = new ScavengerItem[_collectListElements.Length];
        for (var ii = ARRAY_START; ii < _collectListElements.Length; ii++)
        {
            var componentFc = _collectListElements[ii].AddComponent<FakeCollectable>();
            _compRemoveFc.Add(componentFc);
            var componentSi = _collectListElements[ii].AddComponent<ScavengerItem>();
            _compRemoveSi.Add(componentSi);
            onCollectDispatchers[ii] = componentSi;
            componentSi.ID = ii;
        }

        remainingItems = _collectListElements.Length;
        foreach (var t in onCollectDispatchers)
        {
            t.OnObjectDisabled += OnObjectCollected;
        }
    }

    private void NpcDialogueRemaining()
    {
        var singleDialogue = new DialogueClass(PrintNpcText(), DialogueClass.Feel.Calm, npc.transform);

        var newDialogues = new List<DialogueClass>();
        if (wonOnce && !gameIsActive)
        {
            newDialogues.Add(new DialogueClass(winningText, DialogueClass.Feel.Calm, npc.transform));
        }

        newDialogues.Add(singleDialogue);
        _npcDialogue.SetManualDialogue(newDialogues.ToArray());
    }

    private Transform[] RearrangeArray(int size, Transform[] collectList)
    {
        if (size < ARRAY_START || size >= this._collectListElements.Length) return collectList;
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

    private string PrintNpcText()
    {
        var dialogue = "";
        if (!gameIsActive)
        {
            dialogue = wonOnce
                ? "You can keep playing if you want, start on the sign"
                : "Wanna try again? start on the sign";
        }
        else
        {
            dialogue = "The remaining items are:";
            var remainingList = new List<string>();
            for (var ii = ARRAY_START; ii < collectedItems.Length; ii++)
            {
                if (collectedItems[ii]) continue;
                remainingList.Add(_collectListElements[ii].name);
            }

            dialogue += string.Join(",", remainingList) + ".";

            if (remainingList.Count <= NO_REMAINING)
            {
                dialogue = "You collected all the items";
            }
        }

        return dialogue;
    }

    private void CheckAllObjectsAreCollected()
    {
        if (!gameIsActive) return;
        NpcDialogueRemaining();
        if (remainingItems > NO_REMAINING) return;
        OnFinished?.Invoke();
        wonOnce = true;
        gameIsActive = false;
        NpcDialogueRemaining();
        StartCoroutine(WaitAndRestart());
    }

    private IEnumerator WaitAndRestart()
    {
        foreach (var component in _compRemoveFc)
        {
            Destroy(component);
        }

        yield return new WaitForSeconds(RESTART_TIME);
        foreach (var item in onCollectDispatchers)
        {
            item.gameObject.SetActive(true);
        }

        foreach (var component in _compRemoveSi)
        {
            Destroy(component);
        }
    }

    private void GameOver()
    {
        gameIsActive = false;
        NpcDialogueRemaining();
        StartCoroutine(WaitAndRestart());
    }

    #endregion
}