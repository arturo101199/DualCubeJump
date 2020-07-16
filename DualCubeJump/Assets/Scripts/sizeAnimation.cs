using UnityEngine;


public class sizeAnimation : MonoBehaviour
{
    const string TRIGGER = "ChangeSize";
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void makeAnimation()
    {
        anim.SetTrigger(TRIGGER);
    }
}
