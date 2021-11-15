using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityController : MonoBehaviour
{
    [SerializeField]
    private gravityDirection changeTo;
    private AudioSource audio;


    GameObject Bomb;
    gravityDirection gravityDirection;
    Object playerObject;
    Object bombObject;

    private void Awake()
    {
        this.audio = this.gameObject.GetComponent<AudioSource>();
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
        else if (other.CompareTag("Stage"))
        {

        }
        else if (other.CompareTag("Lava"))
        {

        }
        else if (other.CompareTag("BulletLava"))
        {

        }
        else if (other.CompareTag("Player"))
        {
            playerObject = other.GetComponent<Object>();
            playerObject.changeGravity(changeTo);
            gravityDirection = playerObject.gDirection;
            bombObject.gDirection = gravityDirection;
            this.audio.Play();
        }
        else
        {
            other.GetComponent<Object>().changeGravity(changeTo);
        }
    }
}
