using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform heartContainersParent;
    [SerializeField] private int maxContainers;
    [SerializeField] private int currentContainers;
    [SerializeField] private int currentHealth;
    [SerializeField] private HeartContainer[] pooledHealthContainers;
    [SerializeField] private bool isdead;

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
        maxContainers = HeartPooling.SharedInstance.amountToPool;
        InitializeHeartContainers();
        pooledHealthContainers = HeartPooling.SharedInstance.GetPooledArray();
        currentHealth = currentContainers;
    }

    #endregion

    #region Methods

    private void InitializeHeartContainers()
    {
        if (currentContainers < MIN_CONTAINERS)
            currentContainers = MIN_CONTAINERS;
        for (var i = FIRST_INDEX; i < currentContainers; i++)
        {
            AddHeartPooled();
        }
    }

    private void AddHeartPooled()
    {
        if (currentContainers >= maxContainers) return;
        var go = HeartPooling.SharedInstance.GetPooledObject();
        if (go == null) return;
        go.transform.SetParent(heartContainersParent);
        go.SetActive(true);
        currentHealth++;
        UpdateHealth();
    }

    public void TakeDamage()
    {
        if (isdead) return;
        if (currentHealth <= MIN_HEALTH)
        {
            isdead = true;
            OnDeath?.Invoke();
            currentHealth = NO_HEALTH;
            UpdateHealth();
            return;
        }

        currentHealth--;
        UpdateHealth();
    }
    
    private void UpdateHealth()
    {
        for (var index = FIRST_INDEX; index < pooledHealthContainers.Length; index++)
        {
            if (index < currentHealth)
            {
                pooledHealthContainers[index].Healed();
            }
            else
            {
                pooledHealthContainers[index].Damaged();
            }
        }
    }

    #endregion
}