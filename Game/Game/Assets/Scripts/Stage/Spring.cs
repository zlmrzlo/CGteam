using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
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
        var obj = other.gameObject;
        var rigid = obj.GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.AddForce(Vector3.up * 200.0f , ForceMode.Impulse);
        Debug.Log("용수철 점프!");
    }
    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        var rigid = obj.GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.None;
    }
}
