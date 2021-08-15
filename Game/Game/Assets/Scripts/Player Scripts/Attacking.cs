using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    Camera mainCamera;

    Collider target;

    [SerializeField]
    float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        actAttack();
    }

    void actAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //print("act");
            // 스크린의 정중앙
            int x = Screen.width / 2;
            int y = Screen.height / 2;


            // 스크린의 정중앙에 빛을 쏴서 맞추는 오브젝트를 들게 한다.
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                target = hit.collider.GetComponentInChildren<Collider>();
                print(target.name);

                if (target != null)
                {
                    if(!animator.GetBool("Attack"))
                    {
                        animator.SetBool("Attack", true);
                        StartCoroutine(CancelAttack());
                    }
                }
            }
        }
    }

    IEnumerator CancelAttack()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Attack", false);
    }

    void AttackSound()
    {
        audioSource.Play();
    }
}
