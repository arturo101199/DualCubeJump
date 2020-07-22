using UnityEngine;

public enum Gesture { SWIPE_UP, SWIPE_LEFT, SWIPE_RIGHT, CLICK, NONE }

public class GestureDetector
{
    const float SWIPE_THRESHOLD = 75f;
    const float CLICK_THRESHOLD = 10f;
    const float SWIPE_MARGIN = 300f;

    bool rightTouched;
    bool leftTouched;
    float[,] touches = new float[2, 2];

    public void onTouchDown(float x, float y)
    {
        if (x <= Screen.width / 2)
        {
            leftTouched = true;
            touches[0, 0] = x;
            touches[0, 1] = y;
        }
        else
        {
            rightTouched = true;
            touches[1, 0] = x;
            touches[1, 1] = y;
        }
    }

    public (Gesture,bool) onTouchUp(float x, float y)
    {
        float touchX;
        float touchY;
        bool right = false;

        if (x <= Screen.width / 2)
        {
            if (!leftTouched)
                return (Gesture.NONE,right);

            right = false;
            touchX = touches[0, 0];
            touchY = touches[0, 1];
        }
        else
        {
            if (!rightTouched)
                return (Gesture.NONE, right);

            right = true;
            touchX = touches[1, 0];
            touchY = touches[1, 1];
        }

        if (Mathf.Abs(x - touchX) < CLICK_THRESHOLD && Mathf.Abs(y - touchY) < CLICK_THRESHOLD)
        {
            resetSide(right);
            return (Gesture.CLICK, right);
        }

        else if (y - touchY > SWIPE_THRESHOLD && Mathf.Abs(x - touchX) < SWIPE_MARGIN)
        {
            resetSide(right);
            return (Gesture.SWIPE_UP, right);
        }

        else if (x - touchX > SWIPE_THRESHOLD && Mathf.Abs(y - touchY) < SWIPE_MARGIN)
        {
            resetSide(right);
            return (Gesture.SWIPE_RIGHT, right);
        }

        else if (touchX - x > SWIPE_THRESHOLD && Mathf.Abs(y - touchY) < SWIPE_MARGIN)
        {
            resetSide(right);
            return (Gesture.SWIPE_LEFT, right);
        }

        return (Gesture.NONE, right);
    }

    void resetSide(bool right)
    {
        if (right)
            rightTouched = false;
        else
            leftTouched = false;
    }


}
