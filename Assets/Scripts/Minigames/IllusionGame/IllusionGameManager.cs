using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionGameManager : MonoBehaviour
{
    #region Fields

    public GameObject clock;

    public GameObject[] items;
    
    public Transform[] itemSpawnPoints;
    
    private HealthManager healthManager;

    [SerializeField] private TimerScriptableObject timerSo;

    [SerializeField] private Transform npc;

    [SerializeField] private bool gameIsActive;

    [SerializeField] private bool wonOnce;

    [SerializeField] private string winningText;

    private DialogueClass[] _manualDialogues;

    private Dialogue _npcDialogue;

    private Player player;

    #endregion

    #region UnityMethods

    private void Start()
    {
        clock.SetActive(false);
        healthManager = GameObject.Find("HealthManager").GetComponent<HealthManager>();
        gameIsActive = false;
        _npcDialogue = npc.GetComponent<Dialogue>();

        if (Player.Instance != null) player = Player.Instance;

        wonOnce = (player) ? player.magiciansRespect : false;
        if (wonOnce) NpcDialogueRemaining();
    }

    private void OnEnable()
    {
        TimerManager.OnTimeOver += GameOver;
        HealthManager.OnDeath += GameOver;
    }


    private void OnDisable()
    {
        TimerManager.OnTimeOver -= GameOver;
        HealthManager.OnDeath -= GameOver;
    }

    #endregion

    #region Methods

    public bool IsGameActive()
    {
        return gameIsActive;
    }

    public void StartGame()
    {
        if (gameIsActive) return;
        clock.SetActive(true);
        timerSo.StartTimer();

        if(GameManagerScript.Instance != null)
            GameManagerScript.Instance.setGameState(GameManagerScript.GameState.IllusionGame);

        SetObjects();
        gameIsActive = true;
        
        healthManager.StartGame();
        NpcDialogueRemaining();
    }

    private void NpcDialogueRemaining()
    {
        var singleDialogue = new DialogueClass(PrintNpcText(), DialogueClass.Feel.Calm, npc.GetChild(2));

        var newDialogues = new List<DialogueClass>();
        if (wonOnce && !gameIsActive)
        {
            newDialogues.Add(new DialogueClass(winningText, DialogueClass.Feel.Calm, npc.GetChild(2)));
        }

        newDialogues.Add(singleDialogue);
        if (_npcDialogue != null) _npcDialogue.SetManualDialogue(newDialogues.ToArray());
    }

    private string PrintNpcText()
    {
        var dialogue = "";
        if (!gameIsActive)
        {
            dialogue = wonOnce
                ? "Would you like to try my game again? Well of course you do."
                : "Ready to try again? I admire your tenacity!";
        }
        else
        {
            dialogue = "You can do it! Pick which is not like the others.";
        }

        return dialogue;
    }

    private void GameOver()
    {
        healthManager.EndGame();
        gameIsActive = false;
        timerSo.EndTimer();
        clock.SetActive(false);
        NpcDialogueRemaining();

        if (GameManagerScript.Instance != null)
            GameManagerScript.Instance.setGameState(GameManagerScript.GameState.MagiciansFaction);

        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void WonGame()
    {
        wonOnce = true;
        if(player)
            player.magiciansRespect = true;

        GameOver();
    }

    public void SetObjects()
    {
        List<int> spawnPointsTaken = new List<int>();

        foreach (var item in items)
        {
            int i = Random.Range(0, itemSpawnPoints.Length);
            while(spawnPointsTaken.Contains(i))
            {
                i = Random.Range(0, itemSpawnPoints.Length);
            }
            spawnPointsTaken.Add(i);

            item.gameObject.SetActive(true);
            item.transform.position = itemSpawnPoints[i].position;

            item.GetComponent<IllusionItem>().EnableInteract();
        }
    }

    #endregion
}
