using System;
using UnityEngine;

[CreateAssetMenu]
public class Param1EventSO : ScriptableObject
{
    public event Action<bool> actionEvent;

    public void InvokeEvent(bool right)
    {
        if (actionEvent != null)
            actionEvent(right);
    }
}
