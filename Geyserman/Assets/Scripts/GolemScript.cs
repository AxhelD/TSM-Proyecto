using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class GolemScript : MonoBehaviour
{
    public bool startRunning;
    public bool throwing;
    public is throwing = false;
    public int damageToPlayer;
    public float distanceTolerance = 0.5f:
    public float minSpeed;
    public float maxSpeed;
    public float timeToExplode = 8;
    public AudioClip[] runninClip;
    public AudioClip[] throwingClip;
    public AudioClip[] stepClips;
    public AudioClip[] dieClip;

    private bool hasDied;
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource + GetComponent<AudioSource>();
        animator = Getcomponet<Animator>();
        navMeshAgent.speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        
    }
}
