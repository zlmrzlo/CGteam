using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour
{
    Animator animator;
    GameObject player;

    [SerializeField]
    Camera mainCamera;

    Button buttonOnOff;
    Pushable p;
    AudioSource buttonSound;
    Material buttonMaterial;
    bool clickButton;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator != null)
        {
            //print("clear");
        }
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        TryPushing();
    }

    void TryPushing()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            animator.SetBool("Push", true);
        }
    }

    void ActPushing()
    {
        // 스크린의 정중앙
        int x = Screen.width / 2;
        int y = Screen.height / 2;


        // 스크린의 정중앙에 빛을 쏴서 맞추는 오브젝트를 들게 한다.
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5.0f))
        {
            if (hit.transform.CompareTag("Switch"))
            {
                p = hit.collider.GetComponent<Pushable>();
                buttonSound = p.GetComponent<AudioSource>();
                buttonOnOff = p.GetComponentInParent<Button>();
                buttonMaterial = p.GetComponent<Renderer>().material;

                if (buttonMaterial != null)
                {
                    //print("clear3");
                }

                if (p != null)
                {
                    //print("clear2");
                    p.transform.Translate(new Vector3(0.0f, -0.02f, 0.0f));
                    buttonSound.Play();

                    //print(buttonMaterial.color);
                    //print(buttonMaterial.color);
                    if (!clickButton)
                    {
                        buttonMaterial.SetColor("_EmissiveColor", new Color(0f, 170.0f, 170.0f, 0f));
                    }
                    else
                    {
                        buttonMaterial.SetColor("_EmissiveColor", new Color(255.0f, 0f, 0f, 0f));
                    }
                    clickButton = !clickButton;
                }
            }
        }
    }

    void CancelPushing()
    {
        animator.SetBool("Push", false);
        if (p != null)
        {
            p.transform.Translate(new Vector3(0.0f, 0.02f, 0.0f));
        }
    }
}
