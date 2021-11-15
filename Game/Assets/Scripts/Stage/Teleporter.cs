using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject destination;

    private Vector3 destPos;

    // Start is called before the first frame update
    void Start()
    {
        destPos = destination.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        obj.transform.position = destPos;
        Debug.Log("teleport : " + destPos);
    }
}
