using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityController : MonoBehaviour
{
    [SerializeField]
    private gravityDirection changeTo;

    GameObject Bomb;
    gravityDirection gravityDirection;
    Object playerObject;
    Object bombObject;

    void Start()
    {
        Bomb = GameObject.Find("Bomb");
        bombObject = Bomb.GetComponent<Object>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boar"))
        {
            return;
        }

        if (other.CompareTag("Bomb"))
        {

        }
        else if (other.CompareTag("Player"))
        {
            playerObject = other.GetComponent<Object>();
            playerObject.changeGravity(changeTo);
            gravityDirection = playerObject.gDirection;
            bombObject.gDirection = gravityDirection;
        }
        else
        {
            other.GetComponent<Object>().changeGravity(changeTo);
        }
    }
}
