﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReset : MonoBehaviour
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
        obj.GetComponent<Object>().changeGravity(gravityDirection.Down);
        Debug.Log("중력 초기화");
    }
}
