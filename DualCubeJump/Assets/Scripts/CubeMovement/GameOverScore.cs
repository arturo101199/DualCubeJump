using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    public IntValue score;
    public string text;
    Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    void Start()
    {
        scoreText.text = text + score.value;
    }

}
