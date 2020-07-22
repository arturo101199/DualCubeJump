using UnityEngine;
using UnityEngine.UI;

public class ShowCross : MonoBehaviour
{
    public BoolValue mute;

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();    
    }

    void Start()
    {
        image.enabled = mute.value;
    }

    public void ShowHideImage()
    {
        image.enabled = !image.enabled;
    }
}
