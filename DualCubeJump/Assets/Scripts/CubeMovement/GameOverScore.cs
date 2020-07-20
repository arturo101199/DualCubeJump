using UnityEngine;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    public IntValue score;
    public string text;
    TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        scoreText.text = text + score.value;
    }

}
