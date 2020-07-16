using UnityEngine;
using UnityEngine.UI;


public class CountDownText : MonoBehaviour
{
    const int MAX_FONT_SIZE = 300;
    const int MIN_FONT_SIZE = 100;

    Text text;
    bool isChangingSize;

    public VoidEventSO ChangeFontSize;
    public VoidEventSO StopChangeFontSize;
    public Param1IntEventSO ChangeCountDownText;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        ChangeFontSize.actionEvent += makeAnimation;
        StopChangeFontSize.actionEvent += stopAnimation;
        ChangeCountDownText.actionEvent += ChangeText;
    }

    void Update()
    {
        if(isChangingSize)
            text.fontSize = (int)Mathf.Lerp(text.fontSize, MIN_FONT_SIZE, Time.unscaledDeltaTime);

    }


    void makeAnimation()
    {
        isChangingSize = true;
        text.fontSize = MAX_FONT_SIZE;
    }

    void stopAnimation()
    {
        isChangingSize = false;
        text.fontSize = MAX_FONT_SIZE;
    }

    void ChangeText(int currentCountDown)
    {
        print(currentCountDown);
        text.text = currentCountDown.ToString();
    }

    void OnDestroy()
    {
        ChangeFontSize.actionEvent -= makeAnimation;
        StopChangeFontSize.actionEvent -= stopAnimation;
        ChangeCountDownText.actionEvent -= ChangeText;
    }
}
