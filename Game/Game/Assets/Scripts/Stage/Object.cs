﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gravityDirection
{
    Down = 0,
    UP,
    Right,
    Left,
    Forward,
    Beheind
};
public class Object : MonoBehaviour
{
    private GameObject obj; // 해당 게임 오브젝트
    [SerializeField]
    private gravityDirection gDirection;

    // Start is called before the first frame update
    void Start()
    {
        obj = this.gameObject;
        gDirection = gravityDirection.Down;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        changeGravity2();
    }
   
    private void changeGravity2()
    {
        switch (gDirection)
        {
            case gravityDirection.Down:
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 0);
                
                
                break;
            case gravityDirection.UP:
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 19.62f, ForceMode.Acceleration);
                
                break;
            case gravityDirection.Right:
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
                obj.GetComponent<Rigidbody>().AddForce(Vector3.right * 9.81f, ForceMode.Acceleration);
               
                break;
            case gravityDirection.Left:
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
                obj.GetComponent<Rigidbody>().AddForce(Vector3.left * 9.81f, ForceMode.Acceleration);
             
                break;
            case gravityDirection.Forward:
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
                obj.GetComponent<Rigidbody>().AddForce(Vector3.forward * 9.81f, ForceMode.Acceleration);
               
                break;
            case gravityDirection.Beheind:
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
                obj.GetComponent<Rigidbody>().AddForce(Vector3.back * 9.81f, ForceMode.Acceleration);
          
                break;
        }
    }
    public void changeGravity(gravityDirection dir)
    {
        gDirection = dir;
    }
}
