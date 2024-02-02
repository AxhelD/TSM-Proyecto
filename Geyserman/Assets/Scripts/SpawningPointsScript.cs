using UnityEngine;

public class SpawningPointsScript : MonoBehaviour
{
    void Update()
    {
        int speed = Random.Range(10, 200);
        transform.Rotate(Vector3.up * speed);
    }
}
