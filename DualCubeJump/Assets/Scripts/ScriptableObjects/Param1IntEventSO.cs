using System;
using UnityEngine;

[CreateAssetMenu]
public class Param1IntEventSO : ScriptableObject
{
    public event Action<int> actionEvent;

    public void InvokeEvent(int value)
    {
        if (actionEvent != null)
            actionEvent(value);
    }
}
