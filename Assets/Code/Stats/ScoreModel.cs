using System;

public class ScoreModel
{
    public event Action<int> OnScoreChanged;

    private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            OnScoreChanged?.Invoke(score);
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}