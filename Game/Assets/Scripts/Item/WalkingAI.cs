using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class WalkingAI : MonoBehaviour
{
    public GameObject fire;

    public Transform target;

    NavMeshAgent agent;

    public Animator anim;

    private bool isWalking;
    private bool isAttacking;

    [SerializeField] float chaseDistance = 5;
    [SerializeField] float attackDistance = 2;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.isVisible == true)
        //    CheckDistance();
        //else if (GameManager.isVisible == false)
        //    Idle();
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < chaseDistance)
        {
            Chase();
            if (distance < attackDistance)
                Attack();
            else if (distance > attackDistance)
                StopAttack();
        }
        else if (distance >= chaseDistance)
            Idle();
    }

    private void Idle()
    {
        isWalking = false;
        anim.SetBool("Walk", isWalking);
        agent.ResetPath();
    }

    private void Attack()
    {
        //isWalking = false;
        // isAttacking = true;
        //anim.SetBool("Walk", isWalking);
        //agent.speed = 2f;
        fire.SetActive(true);
        // yield return new WaitForSeconds(3f);
        // fire.SetActive(false);
        // isAttacking = false;
    }

    private void StopAttack()
    {
        fire.SetActive(false);
    }

    private void Chase()
    {
        isWalking = true;
        anim.SetBool("Walk", isWalking);
        agent.speed = 2f;
        agent.destination = target.transform.position;
    }
}