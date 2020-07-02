using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObstacleType
{
    DefaultObstacle = 0,
    HoleObstacle = 1,
    LyingObstacle = 2
}

public class InnerGround : MonoBehaviour
{
    const float X_POSITION_OFFSET = 2f;
    const float Z_POSITION_OFFSET = 80f;
    const float Z_START_OFFSET_1 = 70f;
    const float Z_START_OFFSET_2 = 110f;

    public FloatValue groundScaleZ;

    string[] obstacles = { "DefaultObstacle", "HoleObstacle", "LyingObstacle"};
    ObjectPooler objectPooler;

    ObstacleType prevObstacle;

    void Awake()
    {
        objectPooler = ObjectPooler.GetInstance();
    }

    public void SpawnObstacles(bool right)
    {
        //Calculating number of obstacles on a single ground
        int n_obstacles = (int)Mathf.Floor(groundScaleZ.value / Z_POSITION_OFFSET);
        float startZ;

        //Starting position for right/left ground
        if (right)
        {
            startZ = transform.position.z - groundScaleZ.value/2 + Z_START_OFFSET_1;
        }

        else
        {
            startZ = transform.position.z - groundScaleZ.value / 2 + Z_START_OFFSET_2;
        }

        //Spawning Obstacles
        for(int i = 0; i < n_obstacles; i++)
        {
            int randomObstacle;

            if(prevObstacle != ObstacleType.LyingObstacle)
            {
                randomObstacle = Random.Range(0, 5);
            }

            else
            {
                randomObstacle = Random.Range(0, 4);
            }

            ObstacleType obstacle;
            float xPos = transform.position.x;
            if (randomObstacle < 3) //Default Obstacle
            {
                obstacle = ObstacleType.DefaultObstacle;
                int lane = Random.Range(0, 3);
                if (lane == 0)
                    xPos -= X_POSITION_OFFSET;
                else if (lane == 2)
                    xPos += X_POSITION_OFFSET;
            }
            else
            {
                if (randomObstacle == 3)
                    obstacle = ObstacleType.HoleObstacle;
                else
                    obstacle = ObstacleType.LyingObstacle;
            }
            prevObstacle = obstacle;
            Vector3 pos = new Vector3(xPos, 0, startZ + i * Z_POSITION_OFFSET);
            objectPooler.SpawnObject(obstacles[(int)obstacle], pos, Quaternion.identity);
        }
    }
}
