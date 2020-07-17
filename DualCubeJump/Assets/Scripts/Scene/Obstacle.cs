using UnityEngine;

public class Obstacle : MonoBehaviour, IPooledObject
{

    public void OnObjectSpawn()
    {
        transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, transform.position.z);
    }

    // Start is called before the first frame update
    void Update()
    {
        if (Camera.main.transform.position.z > transform.position.z)
        {
            this.gameObject.SetActive(false);
        }
    }

}
