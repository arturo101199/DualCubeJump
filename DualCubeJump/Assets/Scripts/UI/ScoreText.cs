using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    public IntValue score;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        score.value = 0;
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        scoreText.text = "Score: " + score.value;
    }
}
