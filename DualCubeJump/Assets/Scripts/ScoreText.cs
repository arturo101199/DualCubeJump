using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text scoreText;
    public IntValue score;

    void Awake()
    {
        scoreText = GetComponent<Text>();
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
