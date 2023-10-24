using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // uiScore: Điểm người chơi trong Game, winScore/loseScore: điểm hiện ra của của sổ Win/Lose
    [SerializeField] private TextMeshProUGUI uiScore, winScore, loseScore;
    [SerializeField] private TextMeshProUGUI uiCombo;
    private int score = 0;
    private int combo = 1;

    private float doublePointsDuration = 5f;
    private float doublePointsTimer = 0f;
    private bool isDoublePointsActive = false;

    private void Update()
    {
        // Kiểm tra người chơi có đang combo ko và thời gian của nó 
        if (isDoublePointsActive)
        {
            doublePointsTimer -= Time.deltaTime;

            if (doublePointsTimer <= 0f)
            {
                combo = 1;
                isDoublePointsActive = false;
                uiCombo.text = "";
            }
        }
    }


    public void ScorePlus()
    {
        IncreaseScore();
        ActivateDoublePoints();
        uiScore.text = score.ToString();
        winScore.text = score.ToString();
        loseScore.text = score.ToString();
    }

    // Tăng điểm, nếu combo thì điểm tăng + số combo
    public void IncreaseScore()
    {
        if (isDoublePointsActive)
        {
            score = score + combo;
        }
        else
            score++;
    }

    // Trong trạng thái Combo
    public void ActivateDoublePoints()
    {
        combo++;
        isDoublePointsActive = true;
        doublePointsTimer = doublePointsDuration;
        uiCombo.text = "X"+ combo.ToString();
    }
}
