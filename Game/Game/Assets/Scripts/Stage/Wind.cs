using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private Transform nVec;
    [SerializeField] private Transform zeroPos;
    private Vector3 normal;
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
        normal = (nVec.position - zeroPos.position).normalized;
        obj.GetComponent<Rigidbody>().AddForce(normal * 3.0f, ForceMode.Impulse);
    }
}
