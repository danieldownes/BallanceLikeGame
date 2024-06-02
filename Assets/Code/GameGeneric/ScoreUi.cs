using TMPro;
using UnityEngine;

public class ScoreUi : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    public void Init(ScoreModel model)
    {
        model.OnScoreChanged += setScore;
    }

    private void setScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
