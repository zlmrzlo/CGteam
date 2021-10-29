﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    private Vector3 position;
    private Vector3 normal;
    void Start()
    {
        normal = transform.GetChild(0).position;
        position = transform.position;
        direction = (normal - position).normalized;
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
        rigid.AddForce(direction * 200.0f , ForceMode.Impulse);
        Debug.Log("용수철 점프!");
    }
    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        var rigid = obj.GetComponent<Rigidbody>();
        if (obj.CompareTag("Object"))
            rigid.constraints = RigidbodyConstraints.None;
    }
}
