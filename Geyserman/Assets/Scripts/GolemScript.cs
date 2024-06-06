using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class GolemScript : MonoBehaviour
{
    public bool startPursuit;
    public bool isAttacking = false;
    public bool attack;
    public int damageToPlayer;
    public int health;
    public ParticleSystem explosion;
    //public bool isGrounded;

    public float rotationSpeed;
    public float particleSpeed;
    public float heightOffset;
    public float minSpeed;
    public float maxSpeed;
    public float timeToExplode = 5;
    public GameObject spawnPoint;
    public GameObject particlePrefab;
    //private float shootAngle;

    public AudioClip[] throwingClip;
    public AudioClip[] dieClip;
    public AudioClip[] stepClip;
    public AudioClip[] attackClip;

    private bool hasDied;
    private Vector3 targetPosition;
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;
    private GameObject shootedParticle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
        navMeshAgent = GetComponent <NavMeshAgent>();
        navMeshAgent.speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        //int health = GetComponent<EnemyHealthScript>().enemyHealth;

        if (health <= 0 && !hasDied) //Estado de morir
        {
            Die();
            return;
        }

        if (!startPursuit) //Estado de Idle
        {
            Idle();
            return;
        }

        //bool isGrounded = GetComponent<GeysermanMoveScript>().grounded;

        if (health > 0 && !attack && !isAttacking) //Estado de persecucion
        {
            RunTowardsPlayer();
            return;
        }

        if (attack && health > 0) //Estado de atacar
        {
            Attack();
            return;
        }
    }

    private void Idle()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Run", false);
    }

    private void Attack()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("Attack", true);
        animator.SetBool("Run", false);

        targetPosition = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(targetPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }

    private void RunTowardsPlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
        animator.SetBool("Attack", false);
        animator.SetBool("Run", true);
        navMeshAgent.isStopped = false;
    }

    private void Die()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Dead", true);
        int dieClipIndex = Random.Range(0, dieClip.Length);
        audioSource.PlayOneShot(dieClip[dieClipIndex]);
        hasDied = true;
        explosion.Play();
        Destroy(gameObject, explosion.main.duration);
    }

    public void AttackStopped()
    {
        isAttacking = false;
    }

    public void AttackStarted()
    {
        isAttacking = true;
    }

    public void StepSound()
    {
        int stepClipIndex = Random.Range(0, stepClip.Length);
        audioSource.PlayOneShot(stepClip[stepClipIndex]);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attack = false;
        }
    }

    public void Shoot()
    {
        //int attackClipIndex = Random.Range(0, attackClip.Length - 1);
        //audioSource.PlayOneShot(attackClip[attackClipIndex]);
        Vector3 playerPositionOnGround = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 enemyPositionOnGround = new Vector3(spawnPoint.transform.position.x, 0, spawnPoint.transform.position.z);
        Vector3 playerPositionOnVerical = new Vector3(0, player.transform.position.y, 0);
        Vector3 enemyPositionOnVertical = new Vector3(0, spawnPoint.transform.position.y, 0);

        float distanceToPlayerinX = Vector3.Distance(playerPositionOnGround, enemyPositionOnGround);
        float distanceToPlayerinY = Vector3.Distance(playerPositionOnVerical, enemyPositionOnVertical);

        if (player.transform.position.y < spawnPoint.transform.position.y)
        {
            distanceToPlayerinY = (-1 * distanceToPlayerinY) + heightOffset;
        }

        else
        {
            distanceToPlayerinY = distanceToPlayerinY + heightOffset;
        }

        float time = distanceToPlayerinX / particleSpeed;
        float a = (9.81f / 2f) * (time * time);
        float b = -distanceToPlayerinX;
        float c = (a + distanceToPlayerinY);

        float tanA1 = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        float tanA2 = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        float angleA1 = Mathf.Atan(tanA1);
        float angleA2 = Mathf.Atan(tanA2);

        float angleA = 0;
        angleA = Mathf.Min(Mathf.Abs(angleA1), Mathf.Abs(angleA2));

        if (angleA2 > 0)
        {
            angleA = angleA * -1;
        }

        shootedParticle = Instantiate(particlePrefab, spawnPoint.transform.position, Quaternion.identity);
        spawnPoint.transform.localRotation = Quaternion.Euler(new Vector3(angleA * Mathf.Rad2Deg, 0, 0));
        shootedParticle.GetComponent<Rigidbody>().velocity = spawnPoint.transform.forward * particleSpeed;
        return;
    }
}