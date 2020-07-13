using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDetector
{
    const float SWIPE_THRESHOLD = 75f;
    const float CLICK_THRESHOLD = 10f;
    const float SWIPE_MARGIN = 300f;

    float touchX, touchY;
    bool right;

    public enum Gesture { SWIPE_UP, SWIPE_LEFT, SWIPE_RIGHT, CLICK, NONE }

    public bool getRight()
    {
        return right;
    }

    public void onTouchDown(float x, float y)
    {
        touchX = x;
        touchY = y;

        if (touchX <= Screen.width / 2)
            right = false;
        else
            right = true;
    }

    public Gesture onTouchUp(float x, float y)
    {
        if (Mathf.Abs(x - touchX) < CLICK_THRESHOLD && Mathf.Abs(y - touchY) < CLICK_THRESHOLD)
        {
            return Gesture.CLICK;
        }
        /*else if (touchY - y > SWIPE_THRESHOLD && Mathf.Abs(x - touchX) < SWIPE_MARGIN)
        {
            return Gesture.SWIPE_DOWN;
        }*/
        else if (y - touchY > SWIPE_THRESHOLD && Mathf.Abs(x - touchX) < SWIPE_MARGIN)
        {
            return Gesture.SWIPE_UP;
        }
        else if (x - touchX > SWIPE_THRESHOLD && Mathf.Abs(y - touchY) < SWIPE_MARGIN)
        {
            return Gesture.SWIPE_RIGHT;
        }
        else if (touchX - x > SWIPE_THRESHOLD && Mathf.Abs(y - touchY) < SWIPE_MARGIN)
        {
            return Gesture.SWIPE_LEFT;
        }
        return Gesture.NONE;
    }


}
