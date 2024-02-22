using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    public Transform parent;

    void Update()
    {
        if (parent.position.y < 5)
        {
            Vector3 newPosition;
            newPosition.x = parent.position.x;
            newPosition.y = 5;
            newPosition.z = parent.position.z - 30;
            transform.localPosition = newPosition;
        }
        else
        {
            Vector3 newPosition;
            newPosition.x = parent.position.x;
            newPosition.y = parent.position.y;
            newPosition.z = parent.position.z - 30;
            transform.localPosition = newPosition;
        }
    }
}
