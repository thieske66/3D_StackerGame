using System;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public int Score { get; private set; }

    public Action<int> OnScoreChanged;

    public int DebugScore;
    public bool DebugSetScore = false;

    public void Update()
    {
        // DEBUG //
        if (DebugSetScore)
        {
            DebugSetScore = false;
            SetScore(DebugScore);
        }
        ////////////
    }

    public void SetScore(int score)
    {
        this.Score = score;

        OnScoreChanged?.Invoke(Score);
    }

    public void AddScore(int score)
    {
        SetScore(Score + score);
    }
}
