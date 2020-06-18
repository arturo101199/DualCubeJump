using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public VoidEventSO jumpLeftCube;
    public VoidEventSO jumpRightCube;
    public Param1EventSO moveRightCube;
    public Param1EventSO moveLeftCube;

    GestureDetector gestureDetector;

    // Start is called before the first frame update
    void Start()
    {
        gestureDetector = new GestureDetector();
    }

    // Update is called once per frame
    void Update()
    {
        /*MouseJumpInput();
        MobileJumpInput();*/

        MobileInput();
        MouseInput();
        
    }

    void MobileJumpInput()
    {
        if (Input.touchCount > 0)
        {
            float touchX = Input.touches[0].position.x;
            if (touchX <= Screen.width / 2)
            {
                jumpLeftCube.InvokeEvent();
            }
            else
            {
                jumpRightCube.InvokeEvent();
            }
        }
    }

    void MouseJumpInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x <= Screen.width / 2)
            {
                jumpLeftCube.InvokeEvent();
            }
            else
            {
                jumpRightCube.InvokeEvent();
            }
        }
    }

    void MobileInput()
    {
        if (Input.touchCount > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                float touchX = Input.touches[0].position.x;
                float touchY = Input.touches[0].position.y;

                gestureDetector.onTouchDown(touchX, touchY);
            }

            else if(Input.touches[0].phase == TouchPhase.Ended)
            {
                float touchX = Input.touches[0].position.x;
                float touchY = Input.touches[0].position.y;

                bool right = gestureDetector.getRight();

                GetGesture(touchX, touchY);

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

            
            print(gestureDetector.onTouchUp(touchX, touchY));

            GetGesture(touchX, touchY);

        }
        
    }

    void GetGesture(float touchX, float touchY)
    {

        bool right = gestureDetector.getRight();

        switch (gestureDetector.onTouchUp(touchX, touchY))
        {
            case GestureDetector.Gesture.SWIPE_UP:
                if (right)
                    jumpRightCube.InvokeEvent();
                else
                    jumpLeftCube.InvokeEvent();
                return;
            case GestureDetector.Gesture.SWIPE_LEFT:
                if (right)
                    moveRightCube.InvokeEvent(false);
                else
                    moveLeftCube.InvokeEvent(false);
                return;
            case GestureDetector.Gesture.SWIPE_RIGHT:
                if (right)
                    moveRightCube.InvokeEvent(true);
                else
                    moveLeftCube.InvokeEvent(true);
                return;
                //default:
        }
    }
}
