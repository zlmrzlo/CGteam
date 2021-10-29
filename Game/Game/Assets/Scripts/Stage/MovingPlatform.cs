using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 start;
    private Vector3 finish;
    private Vector3 direction;
    [SerializeField]
    private float velocity;
    [SerializeField]
    private SwitchManager[] switchs;
    private int turnOnSwitch;
    private bool switchOn;
    private bool goToFinish;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        start = transform.position;
        finish = transform.GetChild(0).position;
        direction = (finish - start).normalized;
        switchOn = false;
        goToFinish = true;
        //StartCoroutine(MovePlatform());
    }

    void Update()
    {
        CheckSwitch();
        if (switchOn)
            move();
    }
    private void CheckSwitch()
    {
        for (int i = 0; i < switchs.Length; i++)
        {
            if (switchs[i].turnOn == true)
            {
                turnOnSwitch++;
            }
        }
        if (turnOnSwitch == switchs.Length)
        {
            switchOn = true;
        }
        else
        {
            turnOnSwitch = 0;
        }
    }

    private void move()
    {

        if (goToFinish)
        {
            transform.position = Vector3.MoveTowards(transform.position, finish, velocity * Time.deltaTime);
            if (transform.position == finish)
            {
                goToFinish = !goToFinish;
                Debug.Log("플랫폼 이동");
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, start, velocity * Time.deltaTime);
            if (transform.position == start)
            {
                goToFinish = !goToFinish;
                Debug.Log("플랫폼 이동");
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}
