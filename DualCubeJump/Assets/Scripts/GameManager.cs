using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    const int COUNTDOWN_TIME = 3;
    
    [Header("Events")]
    public VoidEventSO OnDisableGround;
    public VoidEventSO onGameOver;

    [Header("GroundScale")]
    public FloatValue groundScaleZ;

    [Header("Scores")]
    public IntValue score;
    public IntValue highScore;

    [Header("Canvas")]
    public GameObject CountDownCanvas;
    public GameObject InGameCanvas;
    public GameObject GameOverCanvas;

    public Text CountDownText;

    ObjectPooler objectPooler;

    float start_position;
    float currentPosition;

    int currentCount = COUNTDOWN_TIME + 1;

    private void Awake()
    {
        objectPooler = ObjectPooler.GetInstance();
    }

    void Start()
    {
        PauseGame();

        highScore.value = PlayerPrefs.GetInt("HighScore", 0);

        EnableEvents();

        SetGroundPositions();

        CreateFirstGrounds();

        StartCoroutine(countDownForStartingGame());
    }

    void SetGroundPositions()
    {
        start_position = groundScaleZ.value / 2f - 10f;
        currentPosition = groundScaleZ.value + start_position;
    }

    void CreateFirstGrounds()
    {
        objectPooler.SpawnObject("Ground", Vector3.forward * start_position, Quaternion.identity);
        objectPooler.SpawnObject("Ground", Vector3.forward * currentPosition, Quaternion.identity);

    }

    void GenerateNewGround()
    {
        currentPosition += groundScaleZ.value;
        objectPooler.SpawnObject("Ground", Vector3.forward * currentPosition, Quaternion.identity);

    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        CountDownCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
    }

    void GameOver()
    {
        PauseGame();
        GameOverCanvas.SetActive(true);
        DisableEvents();
        if (score.value > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score.value);
            highScore.value = score.value;
        }
    }

    void EnableEvents()
    {
        OnDisableGround.actionEvent += GenerateNewGround;
        onGameOver.actionEvent += GameOver;
    }
    
    void DisableEvents()
    {
        OnDisableGround.actionEvent -= GenerateNewGround;
        onGameOver.actionEvent -= GameOver;
    }

    IEnumerator countDownForStartingGame()
    {
        while(currentCount > 1)
        {
            currentCount--;
            CountDownText.text = currentCount.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        ResumeGame();
        StartCoroutine(scoreUpdate());
    }

    IEnumerator scoreUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            score.value++;
        }
    }
}
