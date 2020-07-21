using UnityEngine;

public class ChangeBackgroundColor : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public Color[] colors;

    Camera camera;
    Color backgroundColor;
    Color newBackgroundColor;
    float timer;
    float animationDuration;
    int prevRand = 0;


    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Start()
    {
        ChangeColor();
        animationDuration = animationCurve.keys[animationCurve.length - 1].time;

    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= animationDuration)
        {
            timer = 0;
            ChangeColor();
        }
        InterpolateColor();
    }

    void ChangeColor()
    {
        int rand = Random.Range(0, colors.Length);
        while(rand == prevRand)
            rand = Random.Range(0, colors.Length);
        prevRand = rand;
        backgroundColor = camera.backgroundColor;
        newBackgroundColor = colors[rand];
    }

    void InterpolateColor()
    {
        float timeValue = animationCurve.Evaluate(timer);
        Color interpolatedColor = Color.Lerp(backgroundColor, newBackgroundColor, timeValue);
        camera.backgroundColor = interpolatedColor;
        RenderSettings.fogColor = interpolatedColor;
    }
}
