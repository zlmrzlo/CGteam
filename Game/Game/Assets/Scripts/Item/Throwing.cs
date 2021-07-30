﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    [SerializeField]
    GameObject bomb;

    [SerializeField]
    GameObject parent;

    [SerializeField]
    Camera mainCamera;

    Rigidbody rigidbody;

    Animator animator;

    AudioSource audioSource;

    SphereCollider sphereCollider;

    bool is_Throw = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = bomb.GetComponent<AudioSource>();
        bomb.transform.parent = parent.transform;
        rigidbody = bomb.GetComponent<Rigidbody>();
        sphereCollider = bomb.GetComponent<SphereCollider>();
        if(sphereCollider != null)
        {
            Debug.Log("hit sphereCollider"); 
        }

        animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            //Debug.Log("hit animator"); 
        }
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
        is_Throw = true;
        // 던지는 시점에서의 손의 위치
        audioSource.PlayDelayed(0.5f);
        bomb.transform.position = parent.transform.position;
        bomb.SetActive(true);

        // 부모, 자식 관계 제거
        bomb.transform.parent = null;

        SetLayersRecursively(bomb.transform, "Default");

        // 중력의 영향을 받을 수 있도록 함
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;

        // 몸체의 앞쪽 방향으로 힘을 가함
        rigidbody.AddForce(mainCamera.transform.forward * 50 + mainCamera.transform.up * 25, ForceMode.Impulse);
    }

    public void DeleteBomb()
    {
        animator.SetBool("Throw", false);
        StartCoroutine(Bomb());
        StartCoroutine(RemoveBomb());
    }

    IEnumerator RemoveBomb()
    {
        yield return new WaitForSeconds(3.0f);
        bomb.SetActive(false);
        is_Throw = false;
        sphereCollider.radius = 0.007f; 
        sphereCollider.isTrigger = false;
        rigidbody.isKinematic = true;
    }

    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(2.5f);
        sphereCollider.radius = 1f;
        sphereCollider.isTrigger = true;
        //Debug.Log(sphereCollider.radius);
        //Debug.Log(sphereCollider.isTrigger);
    }

    public void SetLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            SetLayersRecursively(child, name);
        }
    }
}
