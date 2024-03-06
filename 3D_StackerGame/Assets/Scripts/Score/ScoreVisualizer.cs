using UnityEngine;

public class ScoreVisualizer : MonoBehaviour
{
    public int ScoreValue;
    public float TimeToIncrementScore = 1f;

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
}
