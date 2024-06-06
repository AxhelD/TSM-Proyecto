using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public Transform waterJetOrigin;
    public LayerMask collisionLayer; // Capas en las que se debe detectar la colisión

    void Update()
    {
        // Dirección del raycast
        Vector3 direction = transform.position - waterJetOrigin.position;
        direction.Normalize();

        // Longitud del raycast, podrías ajustarla según la distancia que desees
        float distance = Vector3.Distance(transform.position, waterJetOrigin.position);

        // Raycast para detectar la colisión
        RaycastHit hit;
        if (Physics.Raycast(waterJetOrigin.position, direction, out hit, distance, collisionLayer))
        {
            // Si el raycast impacta con el CapsuleCollider del enemigo
            if (hit.collider.CompareTag("EnemyColliderTag"))
            {
                Debug.Log("Colisión detectada entre WaterJet y CapsuleCollider del enemigo.");
            }
        }
    }
}

