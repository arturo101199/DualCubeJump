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
    public VoidEventSO ChangeCountDownSize;
    public VoidEventSO StopChangeCountDownSize;

    [Header("GroundScale")]
    public FloatValue groundScaleZ;

    [Header("Scores")]
    public IntValue score;
    public IntValue highScore;
    
    public Text CountDownText;

    public static bool pause;

    ObjectPooler objectPooler;

    float start_position;
    float currentPosition;

    int currentCount;

    IEnumerator scoreTextRoutine;

    HudSelector hudSelector;
    ScreenFader screenFader;

    private void Awake()
    {
        objectPooler = ObjectPooler.GetInstance();
        hudSelector = GetComponent<HudSelector>();
        screenFader = GetComponentInChildren<ScreenFader>();
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
        hudSelector.setHud(Hud.PAUSE);
        StopCoroutine(scoreTextRoutine);

    }

    public void SetCountDown()
    {
        screenFader.FadeIn();
        currentCount = COUNTDOWN_TIME;
        Time.timeScale = 0f;
        hudSelector.setHud(Hud.COUNT);
        pause = true;
        StartCoroutine(countDownForStartingGame());

    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        hudSelector.setHud(Hud.IN_GAME);
        pause = false;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        hudSelector.setHud(Hud.GAME_OVER);
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
        CountDownText.text = currentCount.ToString();
        yield return new WaitForSecondsRealtime(1f);
        
        while (currentCount > 0)
        {
            ChangeCountDownSize.InvokeEvent();
            CountDownText.text = currentCount.ToString();
            currentCount--;
            yield return new WaitForSecondsRealtime(1f);
        }
        StopChangeCountDownSize.InvokeEvent();
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
