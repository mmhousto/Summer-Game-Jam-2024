using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectGameManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private int maxSearchableItems;

    [SerializeField] private Transform collectListParent;

    [SerializeField] private ScavengerItem[] onCollectDispatchers;

    [SerializeField] private bool[] mustCollectItems;

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
    
    public static event ChangeGameStatusAction OnDamage;
    public static event ChangeGameStatusAction OnFailed;

    #endregion

    #region UnityMethods

    private void OnEnable()
    {
        TimerManager.OnTimeStart += StartGame;
        TimerManager.OnTimeOver += GameOver;
        HealthManager.OnDeath += GameOver;
    }


    private void OnDisable()
    {
        TimerManager.OnTimeStart -= StartGame;
        TimerManager.OnTimeOver -= GameOver;
        HealthManager.OnDeath -= GameOver;
    }

    #endregion

    #region Methods

    private void StartGame()
    {
        if (gameIsActive) return;

        if (GameManagerScript.Instance != null)
            GameManagerScript.Instance.setGameState(GameManagerScript.GameState.CollectionGame);

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

        _collectListElements = RearrangeArray(_collectListElements);
        
        mustCollectItems = new bool[maxSearchableItems];
        onCollectDispatchers = new ScavengerItem[_collectListElements.Length];
        
        for (var ii = ARRAY_START; ii < _collectListElements.Length; ii++)
        {
            var componentFc = _collectListElements[ii].AddComponent<FakeCollectable>();
            _compRemoveFc.Add(componentFc);
            var componentSi = _collectListElements[ii].AddComponent<ScavengerItem>();
            _compRemoveSi.Add(componentSi);
            onCollectDispatchers[ii] = componentSi;
            componentSi.Id = ii;
            componentSi.DoesDamage = ii >= maxSearchableItems;
        }

        remainingItems = maxSearchableItems;
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
        if (_npcDialogue != null) _npcDialogue.SetManualDialogue(newDialogues.ToArray());
    }

    private static Transform[] RearrangeArray(Transform[] collectList)
    {
        for (var ii = ARRAY_START; ii < collectList.Length; ii++)
        {
            var tmp = collectList[ii];
            var r = Random.Range(ii, collectList.Length);
            collectList[ii] = collectList[r];
            collectList[r] = tmp;
        }

        return collectList.ToArray();
    }

    private void OnObjectCollected(int id, bool doesDamage, ScavengerItem t)
    {
        Debug.Log($"Object {id} damages {doesDamage}");
        if (doesDamage)
        {
            OnDamage?.Invoke();
            if (SoundManagerScript.Instance != null)
                SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.collectingDeath);
        }
        else
        {
            if (SoundManagerScript.Instance != null)
            {
                switch (t.tag)
                {
                    case "Barrell":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.wood);
                        break;
                    case "Box":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.wood);
                        break;
                    case "Fork":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.fork);
                        break;
                    case "Hammer":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.hammer);
                        break;
                    case "Potion":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.potion);
                        break;
                    case "Keychain":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.keychain);
                        break;
                    case "Compass":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.compass);
                        break;
                    case "Book":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.book);
                        break;
                    case "Dollhouse":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.dollhouse);
                        break;
                    case "Goldbar":
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.goldbar);
                        break;
                    default:
                        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.collectingWin);
                        break;
                }
            }
                

            mustCollectItems[id] = true;
            remainingItems--;
        }
        t.OnObjectDisabled -= OnObjectCollected;
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
            dialogue = "Missing items:<br>-";
            var remainingList = new List<string>();
            for (var ii = ARRAY_START; ii < mustCollectItems.Length; ii++)
            {
                if (mustCollectItems[ii]) continue;
                remainingList.Add(_collectListElements[ii].name);
            }

            dialogue += string.Join("<br>-", remainingList) + "<br>";

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
        Player.Instance.scavengerRespect = true;
        gameIsActive = false;
        NpcDialogueRemaining();
        StartCoroutine(WaitAndRestart());
    }

    private IEnumerator WaitAndRestart()
    {
        if (_compRemoveFc != null)
            foreach (var component in _compRemoveFc)
            {
                Destroy(component);
            }

        yield return new WaitForSeconds(RESTART_TIME);
        foreach (var item in onCollectDispatchers)
        {
            item.gameObject.SetActive(true);
        }

        if (_compRemoveSi != null)
            foreach (var component in _compRemoveSi)
            {
                Destroy(component);
            }
    }

    private void GameOver()
    {
        if (GameManagerScript.Instance != null)
            GameManagerScript.Instance.setGameState(GameManagerScript.GameState.ScavengersFaction);

        OnFailed?.Invoke();
        gameIsActive = false;
        NpcDialogueRemaining();
        StartCoroutine(WaitAndRestart());
    }

    #endregion
}