using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeartPooling : MonoBehaviour
{
    #region Fields

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    #endregion

    #region Properties

    public static HeartPooling SharedInstance { get; private set; }

    #endregion
    
    #region Constants

    private const int INDEX_START=0;
    private const int LENGTH_TO_INDEX=1;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<GameObject>();
        for (var i = INDEX_START; i < amountToPool; i++)
        {
            var tmp = Instantiate(objectToPool, transform, true);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    #endregion

    #region Methods

    public HeartContainer[] GetPooledArray()
    {
        var pooled = pooledObjects.ToArray();
        var pooledHCs = new List<HeartContainer>(pooled.Length);
        pooledHCs.AddRange(pooled.Select(po => po.GetComponent<HeartContainer>()));
        return pooledHCs.ToArray();
    }

    public GameObject GetPooledObject()
    {
        for (var i = INDEX_START; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    public GameObject GetPooledObjectToRemove()
    {
        for (var i = amountToPool - LENGTH_TO_INDEX; i >= INDEX_START; i--)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    #endregion
}