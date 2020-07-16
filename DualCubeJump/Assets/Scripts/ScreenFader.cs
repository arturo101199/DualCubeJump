using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{

    const string FADE_OUT_TRIGGER = "FadeOut";
    const string FADE_IN_TRIGGER = "FadeIn";
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        anim.SetTrigger(FADE_IN_TRIGGER);
    }

    public void FadeOut()
    {
        anim.SetTrigger(FADE_OUT_TRIGGER);
        
    }

}
