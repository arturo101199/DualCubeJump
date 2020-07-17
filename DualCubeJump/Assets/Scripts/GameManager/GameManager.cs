using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const int COUNTDOWN_TIME = 3;
    
    [Header("Events")]
    public VoidEventSO onDisableGround;
    public VoidEventSO onGameOver;
    public VoidEventSO pauseEvent;
    public VoidEventSO changeCountDownSize;
    public VoidEventSO stopChangeCountDownSize;
    public Param1IntEventSO changeCountDownText;

    [Header("GroundScale")]
    public FloatValue groundScaleZ;

    [Header("Scores")]
    public IntValue score;
    public IntValue highScore;
    
    int currentCount;

    public static bool pause;

    ObjectPooler objectPooler;

    float start_position;
    float currentPosition;

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
        onDisableGround.actionEvent += GenerateNewGround;
        onGameOver.actionEvent += GameOver;
        pauseEvent.actionEvent += PauseGame;
    }
    
    void DisableEvents()
    {
        onDisableGround.actionEvent -= GenerateNewGround;
        onGameOver.actionEvent -= GameOver;
        pauseEvent.actionEvent -= PauseGame;
    }

    IEnumerator countDownForStartingGame()
    {
        changeCountDownText.InvokeEvent(currentCount);
        yield return new WaitForSecondsRealtime(1f);
        
        while (currentCount > 0)
        {
            changeCountDownSize.InvokeEvent();
            yield return new WaitForSecondsRealtime(1f);
            currentCount--;
            changeCountDownText.InvokeEvent(currentCount);
        }
        stopChangeCountDownSize.InvokeEvent();
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
