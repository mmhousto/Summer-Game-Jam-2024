using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerItem : MonoBehaviour
{
    public int ID { get; set; }

    public event System.Action<int, ScavengerItem> OnObjectDisabled;

    private void OnDisable()
    {
        OnObjectDisabled?.Invoke(ID, this);
    }
}