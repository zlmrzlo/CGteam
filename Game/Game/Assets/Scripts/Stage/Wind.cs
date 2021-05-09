using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject;
        obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 3.0f, ForceMode.Impulse);
    }
}
