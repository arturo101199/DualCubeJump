using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public event Action actionEvent;

    public void InvokeEvent()
    {
        if (actionEvent != null)
            actionEvent();
    }
}
