using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    private Vector3 position;
    private Vector3 normal;
    private AudioSource audio;

    public float springPower = 250.0f;
    void Start()
    {
        this.audio = this.transform.GetComponent<AudioSource>();
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
        if (obj.CompareTag("Player"))
            rigid.AddForce(direction * springPower, ForceMode.Impulse);
        else
            rigid.AddForce(direction * 200.0f, ForceMode.Impulse);
        this.audio.Play();
        //Debug.Log("용수철 점프!");
    }
    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        var rigid = obj.GetComponent<Rigidbody>();
        if (obj.CompareTag("Object"))
            rigid.constraints = RigidbodyConstraints.None;
    }
}
