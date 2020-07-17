using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float cameraDistance;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - cameraDistance);
    }
}
