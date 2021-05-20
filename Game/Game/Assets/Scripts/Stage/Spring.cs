using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private Transform nVec;
    [SerializeField] private Transform zeroPos;
    private Vector3 normal;
    // Start is called before the first frame update
    private Vector3 direction;
    private Vector3 position;
    private Vector3 normal;
    void Start()
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        normal = transform.GetChild(0).position;
        position = transform.position;
        direction = (normal - position).normalized;
        
=======
>>>>>>> 066dfb5d008b6fcf9ea5602eb0236c823ee52e62
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        var rigid = obj.GetComponent<Rigidbody>();
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        normal = (nVec.position - zeroPos.position).normalized;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.AddForce(normal * 200.0f , ForceMode.Impulse);
=======
<<<<<<< HEAD
        
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.AddForce(direction * 200.0f , ForceMode.Impulse);
=======
        normal = (nVec.position - zeroPos.position).normalized;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.AddForce(normal * 200.0f , ForceMode.Impulse);
>>>>>>> 066dfb5d008b6fcf9ea5602eb0236c823ee52e62
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
        
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.AddForce(direction * 200.0f , ForceMode.Impulse);
=======
        normal = (nVec.position - zeroPos.position).normalized;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.AddForce(normal * 200.0f , ForceMode.Impulse);
>>>>>>> 066dfb5d008b6fcf9ea5602eb0236c823ee52e62
>>>>>>> Stashed changes
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
