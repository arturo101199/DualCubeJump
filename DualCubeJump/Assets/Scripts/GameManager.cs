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
    public VoidEventSO PauseEvent;

    [Header("GroundScale")]
    public FloatValue groundScaleZ;

    [Header("Scores")]
    public IntValue score;
    public IntValue highScore;

    [Header("Canvas")]
    public GameObject CountDownCanvas;
    public GameObject InGameCanvas;
    public GameObject GameOverCanvas;
    public GameObject PauseCanvas;

    public Text CountDownText;

    public static bool pause;

    ObjectPooler objectPooler;

    float start_position;
    float currentPosition;

    int currentCount;

    IEnumerator scoreTextRoutine;

    private void Awake()
    {
        objectPooler = ObjectPooler.GetInstance();
    }

    void Start()
    {

        highScore.value = PlayerPrefs.GetInt("HighScore", 0);

        scoreTextRoutine = scoreUpdate();

        EnableEvents();

        SetGroundPositions();

        CreateFirstGrounds();

        SetCountDown();
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
        pause = true;
        InGameCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
        StopCoroutine(scoreTextRoutine);

    }

    public void SetCountDown()
    {
        currentCount = COUNTDOWN_TIME + 1;
        Time.timeScale = 0f;
        InGameCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        CountDownCanvas.SetActive(true);
        pause = true;
        StartCoroutine(countDownForStartingGame());

    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        CountDownCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
        pause = false;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
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
        PauseEvent.actionEvent += PauseGame;
    }
    
    void DisableEvents()
    {
        OnDisableGround.actionEvent -= GenerateNewGround;
        onGameOver.actionEvent -= GameOver;
        PauseEvent.actionEvent -= PauseGame;
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
        StartCoroutine(scoreTextRoutine);
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
