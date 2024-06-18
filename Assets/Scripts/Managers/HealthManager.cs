using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region Fields

    [Header("-----Choose initial HP-----")]
    [SerializeField] private int initialHp;
    [SerializeField] private Transform heartContainersParent;

    [Header("-----Readonly-----")]
    [SerializeField] private HeartContainer[] maxPooledHpContainers;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;
    [SerializeField] private bool isdead;

    private int _maxContainers;

    #endregion

    #region Constants

    private const int MIN_CONTAINERS = 1;
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
        _maxContainers = HeartPooling.SharedInstance.amountToPool;
        InitializeHeartContainers();
        currentHp = initialHp;
        maxPooledHpContainers = HeartPooling.SharedInstance.GetPooledArray();
        isdead = false;
    }

    #endregion

    #region Methods

    private void InitializeHeartContainers()
    {
        if (initialHp < MIN_CONTAINERS)
            initialHp = MIN_CONTAINERS;
        if (initialHp > _maxContainers)
            initialHp = _maxContainers;
        for (var i = FIRST_INDEX; i < initialHp; i++)
        {
            AddHeartPooled();
        }
    }

    public void AddHeartPooled()
    {
        if (initialHp > _maxContainers) return;
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
            isdead = true;
            OnDeath?.Invoke();
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

    #endregion
}