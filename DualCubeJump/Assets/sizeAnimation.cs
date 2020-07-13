using UnityEngine;


public class sizeAnimation : MonoBehaviour
{
    const string trigger = "ChangeSize";
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void makeAnimation()
    {
        anim.SetTrigger(trigger);
    }
}
