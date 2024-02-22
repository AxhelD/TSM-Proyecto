using UnityEngine;

public class FollowerCameraScript : MonoBehaviour
{
    public Transform parent;
    public float altY;
    public float distZ;

    void Update()
    {
        if (parent.position.y < altY - 1)
        {
            Vector3 newPosition;
            newPosition.x = parent.position.x;
            newPosition.y = altY;
            newPosition.z = parent.position.z - distZ;
            transform.localPosition = newPosition;
        }
        else
        {
            Vector3 newPosition;
            newPosition.x = parent.position.x;
            newPosition.y = parent.position.y + 1;
            newPosition.z = parent.position.z - distZ;
            transform.localPosition = newPosition;
        }
    }
}
