using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    const float TIME_BETWEEN_CLICKS = 0.5f;

    [Header("InputEvents")]
    public VoidEventSO PauseEvent;
    public VoidEventSO jumpLeftCube;
    public VoidEventSO jumpRightCube;
    public Param1BoolEventSO moveRightCube;
    public Param1BoolEventSO moveLeftCube;

    GestureDetector gestureDetector;
    bool click;
    bool clickRight;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        gestureDetector = new GestureDetector();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.pause)
        {
            if (click) //Handle double click
            {
                timer += Time.deltaTime;
                if (timer > TIME_BETWEEN_CLICKS)
                {
                    click = false;
                    timer = 0;
                }
            }

            MobileInput();
#if UNITY_EDITOR
            MouseInput();
#endif
        }


    }


    void MobileInput()
    {
        if (Input.touchCount > 0)
        {/*
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    float touchX = touch.position.x;
                    float touchY = touch.position.y;

                    gestureDetector.onTouchDown(touchX, touchY);
                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    float touchX = touch.position.x;
                    float touchY = touch.position.y;

                    GetGesture(touchX, touchY);

                }
            }*/
            int touchCount = Input.touchCount;
            for (int i = 0; i < touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    float touchX = touch.position.x;
                    float touchY = touch.position.y;

                    gestureDetector.onTouchDown(touchX, touchY);
                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    float touchX = touch.position.x;
                    float touchY = touch.position.y;

                    GetGesture(touchX, touchY);

                }
            }

        }
    }
    
    void MouseInput()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            float touchX = Input.mousePosition.x;
            float touchY = Input.mousePosition.y;

            gestureDetector.onTouchDown(touchX, touchY);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            float touchX = Input.mousePosition.x;
            float touchY = Input.mousePosition.y;

            GetGesture(touchX, touchY);

        }
        
    }

    void GetGesture(float touchX, float touchY)
    {
        (Gesture, bool) gestureAndSide = gestureDetector.onTouchUp(touchX, touchY);
        Gesture gesture = gestureAndSide.Item1;
        bool right = gestureAndSide.Item2;

        switch (gesture)
        {
            case Gesture.SWIPE_UP:
                if (right)
                    jumpRightCube.InvokeEvent();
                else
                    jumpLeftCube.InvokeEvent();
                return;
            case Gesture.SWIPE_LEFT:
                if (right)
                    moveRightCube.InvokeEvent(false);
                else
                    moveLeftCube.InvokeEvent(false);
                return;
            case Gesture.SWIPE_RIGHT:
                if (right)
                    moveRightCube.InvokeEvent(true);
                else
                    moveLeftCube.InvokeEvent(true);
                return;
            case Gesture.CLICK:
                if (!click)
                {
                    click = true;
                    clickRight = right;
                }
                else
                {
                    if(clickRight == right)
                    {
                        click = false;
                        PauseEvent.InvokeEvent();
                    }
                }
                return;
        }
    }
}
