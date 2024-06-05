using UnityEngine;

public class StartGolemPursuitScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<GolemScript>().startPursuit = true;
        }
    }
}
