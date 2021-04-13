using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    public GameObject Player;
    public GameObject Stone;
    private float Dist;

    [SerializeField]
    float distance;

    void Update()
    {
        Dist = Vector3.Distance(Player.transform.position, Stone.transform.position);
        Fall();
    }

    void Fall()
    {
        if (Dist <= distance)
            Stone.GetComponent<Rigidbody>().useGravity = true;
    }

}
