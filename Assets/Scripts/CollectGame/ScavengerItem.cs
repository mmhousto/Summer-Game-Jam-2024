using UnityEngine;

public class ScavengerItem : MonoBehaviour
{
    #region Fields

    private bool _quitting = false;

    #endregion

    #region Properties

    public int ID { get; set; }

    #endregion

    #region Events

    public event System.Action<int, ScavengerItem> OnObjectDisabled;

    #endregion

    #region UnityMethods

    private void OnDisable()
    {
        if (_quitting) return;
        OnObjectDisabled?.Invoke(ID, this);
    }

    private void OnApplicationQuit()
    {
        _quitting = true;
    }

    #endregion
}