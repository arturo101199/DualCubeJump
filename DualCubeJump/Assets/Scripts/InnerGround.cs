using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerGround : MonoBehaviour
{
    const float X_POSITION_OFFSET = 2f;
    const float Z_POSITION_OFFSET = 80f;
    const float Z_START_OFFSET_1 = 70f;
    const float Z_START_OFFSET_2 = 110f;

    public FloatValue groundScaleZ;

    string[] obstacles = { "DefaultObstacle", "LyingObstacle", "HoleObstacle" };
    ObjectPooler objectPooler;

    void Awake()
    {
        objectPooler = ObjectPooler.GetInstance();
    }

    public void SpawnObstacles(bool right)
    {
        int n_obstacles = (int)Mathf.Floor(groundScaleZ.value / Z_POSITION_OFFSET);
        float startZ;
        if (right)
        {
            startZ = transform.position.z - groundScaleZ.value/2 + Z_START_OFFSET_1;
        }
        else
        {
            startZ = transform.position.z - groundScaleZ.value / 2 + Z_START_OFFSET_2;
        }
        for(int i = 0; i < n_obstacles; i++)
        {
            int obstacle = Random.Range(0, 3);
            float xPos = transform.position.x;
            if(obstacle == 0)
            {
                int lane = Random.Range(0, 3);
                if (lane == 0)
                    xPos -= X_POSITION_OFFSET;
                else if (lane == 2)
                    xPos += X_POSITION_OFFSET;
            }
            Vector3 pos = new Vector3(xPos, 0, startZ + i * Z_POSITION_OFFSET);
            objectPooler.SpawnObject(obstacles[obstacle], pos, Quaternion.identity);
        }
    }
}
