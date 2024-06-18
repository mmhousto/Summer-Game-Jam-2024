using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region Fields

    [Header("-----Choose minigame HP-----")] [SerializeField]
    private int gameHp;

    [SerializeField] private Transform heartContainersParent;

    [Header("-----Readonly-----")]
    [SerializeField] private HeartContainer[] maxPooledHpContainers;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;
    [SerializeField] private bool isdead;
    [SerializeField] private bool isplaying;

    private int _initialHp;
    private int _maxContainers;

    #endregion

    #region Constants

    private const int MIN_CONTAINERS = 0;
    private const int MIN_HEALTH = 1;
    private const int FIRST_INDEX = 0;
    private const int NO_HEALTH = 0;

    #endregion

    #region Events

    public delegate void DieAction();

    public static event DieAction OnDeath;

    #endregion

    #region UnityMethods

    private void Start()
    {
        maxHp = NO_HEALTH;
        _initialHp = NO_HEALTH;
        _maxContainers = HeartPooling.SharedInstance.amountToPool;
        InitializeHeartContainers();
        currentHp = _initialHp;
        isdead = false;
        maxPooledHpContainers = HeartPooling.SharedInstance.GetPooledArray();
    }

    private void OnEnable()
    {
        CollectGameManager.OnStarted += StartGame;
        CollectGameManager.OnFinished += EndGame;
        CollectGameManager.OnFailed += EndGame;
    }


    private void OnDisable()
    {
        CollectGameManager.OnStarted -= StartGame;
        CollectGameManager.OnFinished -= EndGame;
        CollectGameManager.OnFailed -= EndGame;
    }

    #endregion

    #region Methods

    public void StartGame()
    {
        isdead = false;
        _initialHp = gameHp;
        if (_initialHp < MIN_HEALTH)
        {
            _initialHp = MIN_HEALTH;
        }
        InitializeHeartContainers();
        isplaying = true;
    }

    public void EndGame()
    {
        if(isplaying==false||maxHp<=NO_HEALTH)return;
        isplaying = false;
        
        while (maxHp > NO_HEALTH)
        {
            RemoveHeartContainer();
        }
    }

    private void InitializeHeartContainers()
    {
        if (_initialHp < MIN_CONTAINERS)
            _initialHp = MIN_CONTAINERS;
        if (_initialHp == MIN_CONTAINERS) return;
        if (_initialHp > _maxContainers)
            _initialHp = _maxContainers;
        for (var i = FIRST_INDEX; i < _initialHp; i++)
        {
            AddHeartPooled();
        }
    }

    public void AddHeartPooled()
    {
        if (_initialHp > _maxContainers) return;
        var go = HeartPooling.SharedInstance.GetPooledObject();
        if (go == null) return;
        go.transform.SetParent(heartContainersParent);
        go.SetActive(true);
        maxHp++;
        currentHp++;
        UpdateHealth();
    }

    public void TakeDamage()
    {
        if (isdead) return;
        if (currentHp <= MIN_HEALTH)
        {
            if (isplaying)
            {
                isdead = true;
                OnDeath?.Invoke();
            }
            currentHp = NO_HEALTH;
            UpdateHealth();
            return;
        }

        currentHp--;
        UpdateHealth();
    }

    public void Heal()
    {
        if (isdead) return;
        if (currentHp >= maxHp) return;
        currentHp++;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        for (var index = FIRST_INDEX; index < maxPooledHpContainers.Length; index++)
        {
            if (index < currentHp)
            {
                maxPooledHpContainers[index].Healed();
            }
            else
            {
                maxPooledHpContainers[index].Damaged();
            }
        }
    }

    public void RemoveHeartContainer()
    {
        if (maxHp <= MIN_CONTAINERS) return;
        var go = HeartPooling.SharedInstance.GetPooledObjectToRemove();
        if (go == null) return;
        go.SetActive(false);
        maxHp--;
        if (currentHp <= maxHp) return;
        currentHp = maxHp;
        UpdateHealth();
    }

    #endregion
}