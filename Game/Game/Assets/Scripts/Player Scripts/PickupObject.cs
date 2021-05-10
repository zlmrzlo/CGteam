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

    // Update is called once per frame
    void Update()
    {
        if (carrying)
        {
            carry(carriedObject);
            checkDrop();
            //rotateObject();
        }
        else
        {
            pickup();
        }
    }

    // 물건을 듦과 동시에 물건이 회전함
    void rotateObject()
    {
        carriedObject.transform.Rotate(5, 10, 15);
    }

    // 물건을 캐릭터 앞으로 가지고 옴
    void carry(GameObject o)
    {
        o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + (mainCamera.transform.forward - 0.2f * mainCamera.transform.up) * distance, Time.deltaTime * smooth);
    }

    void pickup()
    {
        // E 버튼을 누르면 픽업을 한다.
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("OK1");
            // 스크린의 정중앙
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            // 스크린의 정중앙에 빛을 쏴서 맞추는 오브젝트를 들게 한다.
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("OK2");
                Pickupable p = hit.collider.GetComponentInChildren<Pickupable>();
                if (p != null)
                {
                    Debug.Log("OK3");
                    carrying = true;
                    carriedObject = p.gameObject;
                    p.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }

    void checkDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dropObject();
        }
    }

    void dropObject()
    {
        carrying = false;
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }
}

