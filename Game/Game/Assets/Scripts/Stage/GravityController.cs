using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityController : MonoBehaviour
{
    [SerializeField]
    private gravityDirection changeTo;

    public GameObject bomb;
    public GameObject player;
    gravityDirection gravityDirection;

    // Start is called before the first frame update
    void Start()
    {
        bomb = GameObject.Find("Bomb");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Object>().changeGravity(changeTo);
            gravityDirection = other.GetComponent<Object>().gDirection;
            bomb.GetComponent<Object>().changeGravity(gravityDirection);
        }
        else
        {
            other.GetComponent<Object>().changeGravity(changeTo);
        }
    }
}
