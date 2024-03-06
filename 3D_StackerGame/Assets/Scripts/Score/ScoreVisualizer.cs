using TMPro;
using UnityEngine;

public class ScoreVisualizer : MonoBehaviour
{
    public int ScoreValue;
    public float TimeToIncrementScore = 1f;
    public TextMeshProUGUI TextComponent;

    private Scoreboard score;
    private int previousScore;
    private int newScore;
    private float scoreIncreaseStartTime = 0f;
    private bool increasingScore = false;

    private void Awake()
    {
        score = GameObject.FindObjectOfType<Scoreboard>();
    }

    private void Start()
    {
        score.OnScoreChanged += updateScore;
        updateText();
    }

    public void Update()
    {
        incrementScore();
    }

    private void incrementScore()
    {
        if (!increasingScore)
        {
            return;
        }

        float timeSinceStartAnimation = Time.realtimeSinceStartup - scoreIncreaseStartTime;
        float timeFactor = timeSinceStartAnimation / TimeToIncrementScore;

        ScoreValue = (int)Mathf.Lerp(previousScore, newScore, timeFactor);
        updateText();

        if (timeFactor >= 1f)
        {
            increasingScore = false;
        }
    }

    private void updateScore(int score)
    {
        previousScore = newScore;
        newScore = score;
        
        scoreIncreaseStartTime = Time.realtimeSinceStartup;
        increasingScore = true;
    }

    private void updateText()
    {
        TextComponent.text = ScoreValue.ToString();
    }
}
