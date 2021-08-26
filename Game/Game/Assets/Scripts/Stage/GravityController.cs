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
    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        bomb = GameObject.Find("Bomb");
        player = GameObject.Find("Player");

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
