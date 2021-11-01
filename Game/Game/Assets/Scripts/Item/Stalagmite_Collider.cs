using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite_Collider : MonoBehaviour
{

    public GameObject Stone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Fall();
        }
    }

    private void Fall()
    {
        Stone.GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject);
        Destroy(Stone, 10);
    }
}
