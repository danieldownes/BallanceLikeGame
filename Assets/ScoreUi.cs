using TMPro;
using UnityEngine;

public class ScoreUi : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
