using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    public static ScoreManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    public int score = 0;
    public TMP_Text scoreText;
    
    private bool doubleScore;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    private void OnEnable()
    {
        DoubleScoreController.OnRageModeChanged += HandleRageModeChanged;
    }

    private void OnDisable()
    {
        DoubleScoreController.OnRageModeChanged -= HandleRageModeChanged;
    }

    private void HandleRageModeChanged(bool isDoubleScoreActive)
    {
        if (isDoubleScoreActive) doubleScore = true;
        else doubleScore = false;
    }
    
    public void YellowEnemyScore()
    {
        if(doubleScore) AddScore(40);
        else AddScore(20);
    }
    public void PurpleEnemyScore()
    {
        if(doubleScore) AddScore(60);
        else AddScore(30);
    }
    public void BlueEnemyScore()
    {
        if(doubleScore) AddScore(20);
        else AddScore(10);
    }
}
