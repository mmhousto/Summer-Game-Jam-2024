using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CollectGameManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject[] collectList;

    [SerializeField] private ScavengerItem[] onDestroyDispatchers;

    [SerializeField] private int remainingItems;

    #endregion

    #region Constants

    private const int NO_REMAINING = 0;

    #endregion

    #region UnityMethods

    private void Start()
    {
        onDestroyDispatchers = new ScavengerItem[collectList.Length];
        for (var i = 0; i < collectList.Length; i++)
        {
            collectList[i].AddComponent<Collectable>();
            var myC = collectList[i].AddComponent<ScavengerItem>();
            onDestroyDispatchers[i] = myC;
            myC.ID = i;
        }

        remainingItems = collectList.Length;
        foreach (var t in onDestroyDispatchers)
        {
            t.OnObjectDestroyed += OnObjectCollected;
        }
    }


    private void OnObjectCollected(int id, ScavengerItem t)
    {
        t.OnObjectDestroyed -= OnObjectCollected;
        Debug.Log($"Id {id} was collected");
        remainingItems--;
        CheckAllObjectsAreCollected();
    }


    private void CheckAllObjectsAreCollected()
    {
        if (remainingItems <= NO_REMAINING)
        {
            Debug.Log("Se destruyeron todos los objetos");    
        }
    }

    #endregion
}