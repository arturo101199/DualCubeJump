using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    const float ROTATION_SPEED = 2f;

    void Update()
    {
        transform.Rotate(Vector3.up * ROTATION_SPEED * Time.deltaTime);
    }
}
