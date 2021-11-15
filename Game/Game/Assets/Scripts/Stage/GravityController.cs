﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityController : MonoBehaviour
{
    [SerializeField]
    private gravityDirection changeTo;
    private AudioSource audio;


    public GameObject Bomb;
    gravityDirection gravityDirection;
    Object playerObject;
    Object bombObject;
    private ParticleSystem particle;

    private void Awake()
    {
        this.audio = this.gameObject.GetComponent<AudioSource>();
        if(Bomb.gameObject == null)
        {
            Bomb = GameObject.Find("Hand.R").transform.Find("Bomb").gameObject;
        }
        bombObject = Bomb.GetComponent<Object>();

        particle = GetComponent<ParticleSystem>();
        var fol = particle.forceOverLifetime;
        switch (changeTo)
        {
            case gravityDirection.Down:
                fol.y = -10;
                break;
            case gravityDirection.UP:
                fol.y = 10;
                break;
            case gravityDirection.Right:
                fol.x = 10;
                break;
            case gravityDirection.Left:
                fol.x = -10;
                break;
            case gravityDirection.Forward:
                fol.z = 10;
                break;
            case gravityDirection.Beheind:
                fol.z = -10;
                break;
        }
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
        else if (other.CompareTag("Object"))
        {
            other.GetComponent<Object>().changeGravity(changeTo);
            this.audio.Play();
        }
    }
}
