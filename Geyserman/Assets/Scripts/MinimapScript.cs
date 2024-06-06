using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public GameObject player;

    private void LastUpdate()
    {
        Vector3 newPosition = player.transform.position;

        newPosition.y = transform.position.y;

        transform.position = newPosition;
    }
}
