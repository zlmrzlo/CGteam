using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCollision : MonoBehaviour
{
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = transform.GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.tag = "Stage";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //print("Player");
            //print("velocity: " + other.GetComponent<PlayerController>().myRigid.velocity);
            other.GetComponent<PlayerController>().myRigid.velocity = new Vector3(0, 0, 0);
            boxCollider.isTrigger = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        boxCollider.isTrigger = true;
    }
}
