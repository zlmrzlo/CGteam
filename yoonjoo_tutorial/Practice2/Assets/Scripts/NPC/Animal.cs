using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField]
    protected string animalName; // 동물 이름
    [SerializeField]
    protected int hp; // 동물 체력
    [SerializeField]
    protected float walkSpeed; // 걷기 속도
    [SerializeField]
    protected float runSpeed; // 뛰기 속도

    protected Vector3 destination; // 목적지


    //상태 변수
    protected bool isWalking; // 걷기 판별
    protected bool isAction; // 행동 판별
    protected bool isRunning; // 뛰기 판별
    protected bool isDead; // 죽음 판별

    [SerializeField]
    protected float walkTime; // 걷기 시간
    [SerializeField]
    protected float waitTime; // 대기 시간
    [SerializeField]
    protected float runTime; // 뛰기 시간
    [SerializeField]
    protected float deadTime; // 시체 유지 시간
    protected float currentTime;

    // 필요 컴포넌트
    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected BoxCollider boxCol;
    protected AudioSource theAudio;
    [SerializeField]
    protected AudioClip[] sound_Normal;
    [SerializeField]
    protected AudioClip sound_Hurt;
    [SerializeField]
    protected AudioClip sound_Dead;
    protected NavMeshAgent nav;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            ElapseTime();
        }
        else
        {
            ElapseTime();
        }    
    }

    protected void Move()
    {
        if (isWalking || isRunning)
            //rigid.MovePosition(transform.position + transform.forward * applySpeed * Time.deltaTime);
            nav.SetDestination(transform.position + destination * 5f);
    }

    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && !isDead)
            {
                ReSet();
            }
            else if(currentTime <= 0 && isDead)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void ReSet()
    {
        isWalking = false;
        isAction = true;
        isRunning = false;
        nav.speed = walkSpeed;
        nav.ResetPath();
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
    }



    protected void TryWalk()
    {
        isWalking = true;
        currentTime = walkTime;
        anim.SetBool("Walking", isWalking);
        nav.speed = walkSpeed;
        Debug.Log("걷기");
    }

    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;
            if (hp <= 0)
            {
                Dead();
                return;
            }
            PlaySE(sound_Hurt);
            anim.SetTrigger("Hurt");
        }
    }
    protected void Dead()
    {
        PlaySE(sound_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");
        currentTime = deadTime;
        
    }
    protected void RandomSound()
    {
        int _random = Random.Range(0, 3); // 일상 사운드 3개
        PlaySE(sound_Normal[_random]);
    }
    protected void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
