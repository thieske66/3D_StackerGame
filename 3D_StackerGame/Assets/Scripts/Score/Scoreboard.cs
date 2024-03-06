using System;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public int Score { get; private set; }

    public Action<int> OnScoreChanged;
    public StackController Stack;
    public int ScorePerStackLayer = 15;


    public int DebugScore;
    public bool DebugSetScore = false;

    private void Awake()
    {
        Stack = GameObject.FindObjectOfType<StackController>();
    }

    private void Start()
    {
        Stack.OnStackUpdate += updateScore;
    }

    private void updateScore(Layer activeLayer, int layerHeight)
    {
        int score = Stack.Stack.LayerCount * ScorePerStackLayer;
        SetScore(score);
    }

    public void SetScore(int score)
    {
        if(Score == score)
        {
            return;
        }

        Score = score;
        OnScoreChanged?.Invoke(Score);
    }

    public void AddScore(int score)
    {
        SetScore(Score + score);
    }
}
