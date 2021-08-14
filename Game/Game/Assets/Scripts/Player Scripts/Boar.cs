using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boar : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;
    StatusController status;
    gravityDirection boarDirection;
    gravityDirection playerDirection;
    gravityDirection boarOriginDirection;

    public int boarDamage = 10;

    [SerializeField]
    Transform[] wayPoint;
    int count = 0;

    public gravityDirection GetGravityDirection()
    {
        return boarOriginDirection;
    }

    public void SetGravityDirection(gravityDirection dir)
    {
        boarDirection = dir;
    }

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        status = target.GetComponent<StatusController>();
        InvokeRepeating("BoarPatrol", 0.0f, 1.0f);
    }

    void Update()
    {
        playerDirection = target.GetComponent<Object>().gDirection;

        // 같은 지면에 위치할 경우
        if (boarDirection == playerDirection)
        {
            BoarChase();
        }
    }

    void BoarPatrol()
    {
        // Way Point에 도달할 때 순간적으로 속도가 0이 되는 것을 이용
        if(nav.velocity == Vector3.zero)
        {
            nav.SetDestination(wayPoint[count++].position);

            if(animator.GetBool("Attack"))
            {
                animator.SetBool("Attack", false);
            }

            if(!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }

            // Way Point에 모두 도달할 경우 처음으로 돌아가도록 하기 위해서
            if (count >= wayPoint.Length)
            {
                count = 0;
            }
        }
    }

    void BoarChase()
    {
        if (nav.destination != target.transform.position)
        {
            nav.SetDestination(target.transform.position);

            if (animator.GetBool("Attack"))
            {
                animator.SetBool("Attack", false);
            }

            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }
        }
        else
        {
            // target의 위치에 도달할 경우 그 위치에 고정하여 공격
            nav.SetDestination(transform.position);
        }

        if (nav.remainingDistance <= 2.5f)
        {
            if (animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", false);
            }

            if (!animator.GetBool("Attack"))
            {
                animator.SetBool("Attack", true);
            }
        }
    }

    public void BoarAttack()
    {
        // 같은 지면에 위치한 경우
        if (boarDirection == playerDirection)
        {
            // 공격하기 적당한 위치에 도달한 경우
            if (nav.remainingDistance <= 2.0f)
            {
                // 플레이어의 체력이 0보다 큰 경우
                if (status.currentHp > 0)
                {
                    status.DecreaseHP(boarDamage);
                }
            }
        }
    }
}
