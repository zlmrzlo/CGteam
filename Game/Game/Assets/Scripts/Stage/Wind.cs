﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
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
    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject;
        obj.GetComponent<Rigidbody>().AddForce(direction * 3.0f, ForceMode.Impulse);
    }
}
