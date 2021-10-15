using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // player's transform
    public Vector3 offset; // offset of camera to player's location
    public float camera_speed; // lower number, slower

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, camera_speed);
    }
}
