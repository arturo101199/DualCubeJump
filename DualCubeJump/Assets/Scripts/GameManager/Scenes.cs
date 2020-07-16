using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    ScreenFader screenFader;

    void Awake()
    {
        screenFader = GetComponent<ScreenFader>();
    }

    public void LoadLevel()
    {
        screenFader.FadeOut();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
