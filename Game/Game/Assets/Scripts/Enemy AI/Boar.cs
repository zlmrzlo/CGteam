using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boar : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    Animator animator;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float reachWalkPoint;

    //Attacking
    public float timeBetweenAttacks;
    public float timeBetweenHurt;
    bool alreadyAttacked;
    bool alreadyHurted;
    bool isDeath;

    public AudioClip Death;
    public AudioClip Injured;
    public AudioClip Attack;
    AudioSource audioSource;

    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!isDeath)
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
                animator.SetBool("Walk", true);
                Patroling();
            }

            if (playerInSightRange && !playerInAttackRange)
            {
                animator.SetBool("Walk", true);
                ChasePlayer();
            }

            if (playerInAttackRange && playerInSightRange)
            {
                if (!alreadyHurted)
                {
                    AttackPlayer();
                }
            }
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < reachWalkPoint)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        float x = player.position.x;
        float z = player.position.z;

        transform.LookAt(new Vector3(x, 0f, z));

        if (!alreadyAttacked && !animator.GetBool("Hurt"))
        {
            ///Attack code here
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);
            audioSource.clip = Attack;
            audioSource.Play();
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        //print("Attack");
        alreadyAttacked = false;
        animator.SetBool("Attack", false);
    }

    public void TakeDamage(int damage)
    {
        if(!alreadyHurted && !isDeath)
        {
            health -= damage;

            animator.SetBool("Attack", false);
            animator.SetBool("Hurt", true);
            alreadyHurted = true;
            audioSource.clip = Injured;
            audioSource.Play();
            Invoke(nameof(ResetHurt), timeBetweenHurt);

            if (health <= 0)
            {
                isDeath = true;
                Invoke(nameof(DestroyEnemy), 3.0f);
                animator.SetTrigger("Death");
                audioSource.clip = Death;
                audioSource.Play();
                //print("Death");
            }
        }
    }

    void ResetHurt()
    {
        //print("Hurt");
        alreadyHurted = false;
        animator.SetBool("Hurt", false);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
