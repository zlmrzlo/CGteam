using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    [SerializeField]
    ParticleSystem smallExplosion;

    [SerializeField]
    GameObject bomb;

    [SerializeField]
    GameObject parent;

    [SerializeField]
    Camera mainCamera;

    Rigidbody rigidbody;
    Rigidbody playerRigidbody;

    Animator animator;

    AudioSource audioSource;
    AudioSource audioSourceFire;

    SphereCollider sphereCollider;

    bool is_Throw = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceFire = smallExplosion.GetComponent<AudioSource>();
        audioSource = bomb.GetComponent<AudioSource>();
        bomb.transform.parent = parent.transform;
        rigidbody = bomb.GetComponent<Rigidbody>();
        sphereCollider = bomb.GetComponent<SphereCollider>();
        playerRigidbody = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (sphereCollider != null)
        {
            //`Debug.Log("hit sphereCollider"); 
        }

        if (animator != null)
        {
            //Debug.Log("hit animator"); 
        }
        bomb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TryThrow();
    }

    void TryThrow()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(is_Throw == false)
            {
                animator.SetBool("Throw", true);
            }
        }
    }

    public void ThrowBomb()
    {
        //Debug.Log("Throw Bomb"); 
        is_Throw = true;
        // 던지는 시점에서의 손의 위치
        bomb.transform.position = parent.transform.position;
        bomb.SetActive(true);
        audioSource.PlayDelayed(0.5f);

        // 부모, 자식 관계 제거
        bomb.transform.parent = null;

        // 중력의 영향을 받을 수 있도록 함
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;

        // 몸체의 앞쪽 방향으로 힘을 가함
        rigidbody.AddForce(mainCamera.transform.forward * 25 + mainCamera.transform.up * 10, ForceMode.Impulse);
    }

    public void DeleteBomb()
    {
        //Debug.Log("Delete Bomb");
        animator.SetBool("Throw", false);
        StartCoroutine(Bomb());
        StartCoroutine(RemoveBomb());
    }

    IEnumerator RemoveBomb()
    {
        //Debug.Log("Remove Bomb");
        yield return new WaitForSeconds(2.5f);
        //Debug.Log("Remove Bomb wait");
        audioSourceFire.Play();
        yield return new WaitForSeconds(0.5f);
        smallExplosion.transform.position = bomb.transform.position;
        smallExplosion.Play();
        yield return new WaitForSeconds(1.5f);
        bomb.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        sphereCollider.radius = 0.007f; 
        sphereCollider.isTrigger = false;
        is_Throw = false;
    }

    IEnumerator Bomb()
    {
        //Debug.Log("Bomb");
        yield return new WaitForSeconds(2.5f);
        //Debug.Log("Bomb wait");
        sphereCollider.radius = 0.5f;
        rigidbody.isKinematic = true;
        sphereCollider.isTrigger = true;
    }
}
