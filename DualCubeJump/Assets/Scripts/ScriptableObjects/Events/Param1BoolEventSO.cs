using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Param1BoolEventSO")]
public class Param1BoolEventSO : ScriptableObject
{
    public event Action<bool> actionEvent;

    public void InvokeEvent(bool right)
    {
        if (actionEvent != null)
            actionEvent(right);
    }
}
