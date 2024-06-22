using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StealthGameManager : MonoBehaviour
{
    #region Fields

    public PlayerInput playerInput;
    GameObject[] enemies;
    public bool playerHasItem;

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        playerHasItem = false;
        playerInput.ActivateInput();
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        TimerManager.OnTimeStart += SetRandomItem;
    }

    private void OnDisable()
    {
        TimerManager.OnTimeStart -= SetRandomItem;
    }

    private void SetRandomItem()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyIndex = Random.Range(0, enemies.Length);
        Enemy randomEnemy = enemies[enemyIndex].GetComponent<Enemy>();
        randomEnemy.item.SetActive(true);
        randomEnemy.hasItem = true;
    }

    #endregion
}
