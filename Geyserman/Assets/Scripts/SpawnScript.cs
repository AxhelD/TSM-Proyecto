using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] prefabs; //prefabricados de los enemigos
    public GameObject[] spawnPoints; //spawners
    public GameObject particleExplosion;
    public AudioClip explosion;

    public float firstTime;
    public float constantTime;
    public float spawnHealth = 500;
    public bool spawnDestroyed = false;

    private int prefabsIndex;
    private int spawnPointsIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvokeRepeating("SpawnEnemy", firstTime, constantTime);
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            prefabsIndex = Random.Range(0, prefabs.Length);
            spawnPointsIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(prefabs[prefabsIndex], spawnPoints[spawnPointsIndex].transform.position, spawnPoints[spawnPointsIndex].transform.rotation);
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (spawnHealth <= 0)
        {
            spawnDestroyed = true;
            Instantiate(particleExplosion, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(explosion);
            Destroy(gameObject);
        }
    }
}
