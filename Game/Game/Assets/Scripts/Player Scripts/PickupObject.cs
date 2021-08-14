using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    bool carrying;
    GameObject carriedObject;
    public float distance;
    public float smooth; 
    Pickupable p;

    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!GameManager.isPause)
        {
            if (carrying)
            {
                Carry(carriedObject);
                CheckDrop();
            }
            else
            {
                Pickup();
            }
        }
    }

    // 물건을 캐릭터 앞으로 가지고 옴
    void Carry(GameObject o)
    {
        // Vector3.Lerp(물건 위치, 최종 위치, 인터벌)
        o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + (0.8f * mainCamera.transform.forward - 0.3f * mainCamera.transform.up) * distance, Time.deltaTime * smooth);
    }

    void Pickup()
    {
        // E 버튼을 누르면 픽업을 한다.
        if (Input.GetKeyDown(KeyBindManager.KeyBinds["ACT"]))
        {
            // 스크린의 정중앙
            int x = Screen.width / 2;
            int y = Screen.height / 2;


            // 스크린의 정중앙에 빛을 쏴서 맞추는 오브젝트를 들게 한다.
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                p = hit.collider.GetComponentInChildren<Pickupable>();
                
                if (p != null)
                {
                    animator.SetBool("Hold", true);
                    StartCoroutine(PickDelay());
                }
            }
        }
    }

    IEnumerator PickDelay()
    {
        yield return new WaitForSeconds(0.1f);
        carrying = true;
        carriedObject = p.gameObject;
        p.GetComponent<Rigidbody>().isKinematic = true;
        p.GetComponent<BoxCollider>().enabled = false;
    }

    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
        }
    }

    void DropObject()
    {
        carrying = false;
        animator.SetBool("Hold", false);
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        p.GetComponent<BoxCollider>().enabled = true;
        carriedObject = null;
    }
}

