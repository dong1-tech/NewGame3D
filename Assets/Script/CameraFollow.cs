using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform CameraFollowPoint;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = CameraFollowPoint.position;
        transform.rotation = CameraFollowPoint.rotation;
    }
}
