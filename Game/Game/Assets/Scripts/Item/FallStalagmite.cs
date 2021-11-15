using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallStalagmite : MonoBehaviour
{
    public GameObject Stone;
    public float maxDistance = 20.0f;
    RaycastHit hit;

    private void Update()
    {
        TryFall();
    }

    void TryFall()
    {
        Debug.DrawRay(transform.position, -transform.up * maxDistance, Color.blue, 1.0f);
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxDistance))
        {
            //print(hit.transform.name);
            if (hit.transform.name == "Player")
            {
                //print("Fall");
                Fall();
            }
        }
    }
    private void Fall()
    {
        Stone.GetComponent<Rigidbody>().isKinematic = false;
        Stone.GetComponent<Rigidbody>().useGravity = true;
        //Destroy(gameObject);
        //Destroy(Stone, 10);
    }
}
