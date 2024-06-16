using UnityEngine;

[CreateAssetMenu(fileName = "TimerScriptableObject", menuName = "ScriptableObjects/Timer")]
public class TimerScriptableObject : ScriptableObject
{
    #region Fields

    public int minutes;
    public int seconds;
    private bool _editingText;

    #endregion

    #region Properties

    public int Minutes
    {
        get => minutes;
        set => minutes = value;
    }

    public int Seconds
    {
        get => seconds;
        set => seconds = value;
    }

    #endregion

    #region Constants

    private const int MINUTE_TO_SECONDS = 60;
    private const int MAX_MINUTES = 59;

    #endregion

    #region Events
    public delegate void StartAction(TimerScriptableObject timer);
    public delegate void StartGameAction();
    public delegate void EndAction();

    public static event StartAction OnStart;
    
    public static event EndAction OnInterrupt;
    #endregion
    
    #region UnityMethods

    private void OnValidate()
    {
#if UNITY_EDITOR
        ValidateTime();
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    #endregion

    #region Methods

    private void ValidateTime()
    {
        if (seconds < MINUTE_TO_SECONDS && minutes < MAX_MINUTES) return;
        var mins = Mathf.FloorToInt(((float)seconds / MINUTE_TO_SECONDS));
        seconds %= MINUTE_TO_SECONDS;
        minutes += mins;
        minutes = Mathf.Min(minutes, MAX_MINUTES);
    }
    
    public void StartTimer()
    {
        OnStart?.Invoke(this);
    }
    
    public void EndTimer()
    {
        OnInterrupt?.Invoke();
    }

    #endregion
}