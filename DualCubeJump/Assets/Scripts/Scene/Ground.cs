using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, IPooledObject
{

    public VoidEventSO OnDisableGround;
    public FloatValue scaleZ;

    void Update()
    {
        if (Camera.main.transform.position.z > transform.position.z + scaleZ.value / 2)
        {
            OnDisableGround.InvokeEvent();
            this.gameObject.SetActive(false);
        }
    }
    
    public void OnObjectSpawn()
    {
        InnerGround[] innerGrounds = transform.GetComponentsInChildren<InnerGround>();
        bool right = false;
        foreach (InnerGround innerGround in innerGrounds)
        {
            innerGround.SpawnObstacles(right);
            right = !right;
        }
    }
}
