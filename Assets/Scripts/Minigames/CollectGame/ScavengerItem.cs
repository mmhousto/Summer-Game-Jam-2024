using UnityEngine;

public class ScavengerItem : MonoBehaviour
{
    #region Fields

    private bool _quitting;

    #endregion

    #region Properties

    public int Id { get; set; }
    public bool DoesDamage { get; set; }

    #endregion

    #region Events

    public event System.Action<int,bool, ScavengerItem> OnObjectDisabled;

    #endregion

    #region UnityMethods

    private void OnDisable()
    {
        if (_quitting) return;
        OnObjectDisabled?.Invoke(Id, DoesDamage ,this);
    }

    private void OnApplicationQuit()
    {
        _quitting = true;
    }

    #endregion
}