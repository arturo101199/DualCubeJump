using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Param1IntEventSO")]
public class Param1IntEventSO : ScriptableObject
{
    public event Action<int> actionEvent;

    public void InvokeEvent(int value)
    {
        if (actionEvent != null)
            actionEvent(value);
    }
}
