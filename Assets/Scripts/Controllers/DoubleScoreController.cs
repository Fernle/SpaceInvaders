using UnityEngine;

public class DoubleScoreController : MonoBehaviour
{
    #region Singleton
    public static DoubleScoreController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    public delegate void ModeChangedDelegate(bool isDoubleScoreActive);
    public static event ModeChangedDelegate OnRageModeChanged;

    private bool isDoubleScoreActive;

    public bool IsDoubleScoreActive
    {
        get { return isDoubleScoreActive; }
        set
        {
            if (isDoubleScoreActive != value)
            {
                isDoubleScoreActive = value;
                OnRageModeChanged?.Invoke(isDoubleScoreActive);
            }
        }
    }
    public void ToggleMode()
    {
        IsDoubleScoreActive = !IsDoubleScoreActive;
    }
}
