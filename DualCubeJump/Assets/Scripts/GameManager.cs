using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    const int COUNTDOWN_TIME = 3;

    public GameObject ground;
    public VoidEventSO OnDisableGround;
    public FloatValue groundScaleZ;
    public GameObject CountDownCanvas;
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

        OnDisableGround.actionEvent += GenerateNewGround;

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
    }

    IEnumerator countDownForStartingGame()
    {
        while(currentCount > 0)
        {
            currentCount--;
            CountDownText.text = currentCount.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        ResumeGame(); 
    }
}
