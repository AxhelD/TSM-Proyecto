using UnityEngine;

public class FollowerCameraScript : MonoBehaviour
{
    public Transform parent;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public float altY;
    public float distZ;
    private Vector3 newPosition;

    void Update()
    {
        newPosition.x = parent.position.x;
        newPosition.y = parent.position.y + 1;
        newPosition.z = parent.position.z - distZ;

        if (parent.position.x < xMin)
        {
            newPosition.x = xMin;
        }

        if (parent.position.x > xMax)
        {
            newPosition.x = xMax;
        }

        if (parent.position.y < altY - 1)
        {
            if (parent.position.y < yMin)
            {
                newPosition.y = parent.position.y + altY;
            }
            else
            {
                newPosition.y = altY;
            }
        }
       
        if (parent.position.y > yMax)
        {
            newPosition.y = yMax;
        }

        transform.localPosition = newPosition;
    }
}
