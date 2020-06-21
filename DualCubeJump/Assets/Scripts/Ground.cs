using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public VoidEventSO OnDisableGround;
    public FloatValue scaleZ;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.z > transform.position.z + scaleZ.value / 2)
        {
            OnDisableGround.InvokeEvent();
            this.gameObject.SetActive(false);
        }
    }
}
