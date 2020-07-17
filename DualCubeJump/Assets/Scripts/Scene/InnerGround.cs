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

    const int DEFAULT_PROB = 70;
    const int HOLE_PROB = 8;
    const int LYING_PROB = 22;

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

            //Avoiding two lying obstacles in a row
            if(prevObstacle != ObstacleType.LyingObstacle)
            {
                randomObstacle = Random.Range(0, 100);
            }

            else
            {
                randomObstacle = Random.Range(0, 100 - LYING_PROB);
            }

            ObstacleType obstacle;
            float xPos = transform.position.x;

            //Choosing randomly the type of the obstacle
            if (randomObstacle < DEFAULT_PROB)
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
                if (randomObstacle < DEFAULT_PROB + HOLE_PROB)
                {
                    obstacle = ObstacleType.HoleObstacle;
                }
                else
                {
                    obstacle = ObstacleType.LyingObstacle;
                }
            }

            prevObstacle = obstacle;
            Vector3 pos = new Vector3(xPos, 0, startZ + i * Z_POSITION_OFFSET);
            objectPooler.SpawnObject(obstacles[(int)obstacle], pos, Quaternion.identity);
        }
    }
}
