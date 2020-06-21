using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject ground;
    public VoidEventSO OnDisableGround;
    public FloatValue groundScaleZ;

    ObjectPooler objectPooler;

    float start_position;
    float currentPosition;

    private void Awake()
    {
        objectPooler = ObjectPooler.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnDisableGround.actionEvent += GenerateNewGround;
        start_position = groundScaleZ.value / 2f - 10f;
        currentPosition = groundScaleZ.value + start_position;
        CreateFirstGrounds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateFirstGrounds()
    {
        objectPooler.SpawnObject("Ground", Vector3.forward * start_position, Quaternion.identity);
        objectPooler.SpawnObject("Ground", Vector3.forward * currentPosition, Quaternion.identity);

    }

    void GenerateNewGround()
    {
        currentPosition += groundScaleZ.value;
        objectPooler.SpawnObject("Ground", Vector3.forward * currentPosition, Quaternion.identity);

    }
}
