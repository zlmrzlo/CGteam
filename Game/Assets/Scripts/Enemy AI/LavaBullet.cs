using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBullet : MonoBehaviour
{
    StatusController status;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //print("Player");
            status = other.GetComponent<StatusController>();
            float damage = Random.Range(5f, 50f);
            status.DecreaseHP((int)damage);
        }
    }
}
