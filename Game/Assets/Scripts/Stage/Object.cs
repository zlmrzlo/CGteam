using System.Collections;
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
    GameObject obj; // 해당 게임 오브젝트
    [SerializeField] public gravityDirection gDirection;
    Rigidbody myRigid;

    void Start()
    {
        obj = this.gameObject;
        if(gDirection == gravityDirection.Down)
        {
            gDirection = gravityDirection.Down;
        }
        myRigid = obj.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        changeGravity2();
    }
   
    private void changeGravity2()
    {
        switch (gDirection)
        {
            case gravityDirection.Down:
                myRigid.AddForce(Vector3.up * 0);
                break;
            case gravityDirection.UP:
                myRigid.AddForce(Vector3.up * 19.62f, ForceMode.Acceleration);     
                break;
            case gravityDirection.Right:
                myRigid.AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
                myRigid.AddForce(Vector3.right * 9.81f, ForceMode.Acceleration);
                break;
            case gravityDirection.Left:
                myRigid.AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
                myRigid.AddForce(Vector3.left * 9.81f, ForceMode.Acceleration);
                break;
            case gravityDirection.Forward:
                myRigid.AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
                myRigid.AddForce(Vector3.forward * 9.81f, ForceMode.Acceleration);
                break;
            case gravityDirection.Beheind:
                myRigid.AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
                myRigid.AddForce(Vector3.back * 9.81f, ForceMode.Acceleration);
                break;
        }
    }

    public void changeGravity(gravityDirection dir)
    {
        if(gDirection != dir)
        {
            gDirection = dir;
            changeDirection();
        }
    }

    public void changeDirection()
    {
        if(transform.CompareTag("Player"))
        {
            switch (gDirection)
            {
                case gravityDirection.Down:
                    myRigid.rotation = new Quaternion(0f, 0f, 0f, 1.0f).normalized;
                    myRigid.MoveRotation(myRigid.rotation);
                    break;
                case gravityDirection.UP:
                    myRigid.rotation = new Quaternion(-1.0f, 0f, 0f, 0f).normalized;
                    myRigid.MoveRotation(myRigid.rotation);
                    break;
                case gravityDirection.Right:
                    myRigid.rotation = new Quaternion(0f, 0f, 0.7f, 0.7f).normalized;
                    myRigid.MoveRotation(myRigid.rotation);
                    break;
                case gravityDirection.Left:
                    myRigid.rotation = new Quaternion(0f, 0f, -0.7f, 0.7f).normalized;
                    myRigid.MoveRotation(myRigid.rotation);
                    break;
                case gravityDirection.Forward:
                    myRigid.rotation = new Quaternion(-0.7f, 0f, 0f, 0.7f).normalized;
                    myRigid.MoveRotation(myRigid.rotation);
                    break;
                case gravityDirection.Beheind:
                    myRigid.rotation = new Quaternion(-0.7f, 0f, 0f, -0.7f).normalized;
                    myRigid.MoveRotation(myRigid.rotation);
                    break;
            }
        }
    }
}
