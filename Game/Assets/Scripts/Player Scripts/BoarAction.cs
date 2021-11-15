using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAction : MonoBehaviour
{
    [SerializeField] string boarName;
    [SerializeField] int hp;

    [SerializeField] float walkSpeed;

    bool isWalk;
    bool isAction;

    [SerializeField] float walkTime;
    [SerializeField] float waitTime;
    float currentTime;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] BoxCollider boxCollider;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
        isAction = true;
        isWalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();
        ElapseTime();
    }

    void Move()
    {
        if(isWalk)
        {
            rigidbody.MovePosition(transform.position + transform.forward * walkSpeed * Time.deltaTime);
        }
    }

    void ElapseTime()
    {
        if(isAction)
        {
            currentTime -= Time.deltaTime;

            if(currentTime <= 0)
            {
                Reset();
            }
        }
    }

    void Reset()
    {
        isWalk = false;
        animator.SetBool("Walk", isWalk);
        animator.SetBool("Eat", false);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        isAction = true;
        RandomAction();
    }

    void Rotation()
    {
        if(isAction)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rigidbody.MoveRotation(Quaternion.Euler(rotation));
        }
    }

    void RandomAction()
    {
        // 매개변수의 자료형에 따라 범위가 달라짐
        // int 자료형은 최댓값 미포함
        // float 자료형은 최댓값 포함
        int random = Random.Range(0, 3);
        
        if(random == 0)
        {
            // 걷기
            Walk();
        }
        else if(random == 1)
        {
            // 풀 뜯기
            Eat();
        }
        else if(random == 2)
        {
            // 대기
            Idle();
        }
    }

    void Walk()
    {
        isWalk = true;
        animator.SetBool("Walk", isWalk);
        currentTime = walkTime;
        Debug.Log("걷기");
    }

    void Eat()
    {
        currentTime = waitTime;
        animator.SetBool("Eat", true);
        Debug.Log("풀 뜯기");
    }

    void Idle()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }
}
