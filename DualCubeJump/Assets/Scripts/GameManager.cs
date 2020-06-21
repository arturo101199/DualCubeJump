using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ground;
    public VoidEventSO OnDisableGround;
    public FloatValue groundScaleZ;

    const float START_POSITION = 300f;

    float currentPosition = 1000f;



    // Start is called before the first frame update
    void Start()
    {
        CreateFirstGrounds();
        OnDisableGround.actionEvent += GenerateNewGround;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateFirstGrounds()
    {
        Instantiate(ground, Vector3.forward * START_POSITION, Quaternion.identity);
        Instantiate(ground, Vector3.forward * currentPosition, Quaternion.identity);
    }

    void GenerateNewGround()
    {
        currentPosition += groundScaleZ.value;
        Instantiate(ground, Vector3.forward * currentPosition, Quaternion.identity);

    }
}
