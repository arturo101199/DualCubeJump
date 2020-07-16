using UnityEngine;
using UnityEngine.UI;


public class CountDownText : MonoBehaviour
{
    Text text;
    bool isChangingSize;

    public VoidEventSO ChangeFontSize;
    public VoidEventSO StopChangeFontSize;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        ChangeFontSize.actionEvent += makeAnimation;
        StopChangeFontSize.actionEvent += stopAnimation;
    }

    void Update()
    {
            if(isChangingSize)
                text.fontSize = (int)Mathf.Lerp(text.fontSize, 150f, Time.unscaledDeltaTime);
    }


    public void makeAnimation()
    {
        isChangingSize = true;
        text.fontSize = 300;
    }

    public void stopAnimation()
    {
        isChangingSize = false;
        text.fontSize = 300;
    }

    void OnDestroy()
    {
        ChangeFontSize.actionEvent -= makeAnimation;
        StopChangeFontSize.actionEvent -= stopAnimation;
    }
}
