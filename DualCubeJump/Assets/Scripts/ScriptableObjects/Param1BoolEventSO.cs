using System;
using UnityEngine;

[CreateAssetMenu]
public class Param1BoolEventSO : ScriptableObject
{
    public event Action<bool> actionEvent;

    public void InvokeEvent(bool right)
    {
        if (actionEvent != null)
            actionEvent(right);
    }
}
