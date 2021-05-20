using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
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
=======
<<<<<<< HEAD
        normal = transform.GetChild(0).position;
        position = transform.position;
        direction = (normal - position).normalized;
=======
>>>>>>> 066dfb5d008b6fcf9ea5602eb0236c823ee52e62
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject;
<<<<<<< Updated upstream
        normal = (nVec.position - zeroPos.position).normalized;
        obj.GetComponent<Rigidbody>().AddForce(normal * 3.0f, ForceMode.Impulse);
=======
<<<<<<< HEAD
        obj.GetComponent<Rigidbody>().AddForce(direction * 3.0f, ForceMode.Impulse);
=======
        normal = (nVec.position - zeroPos.position).normalized;
        obj.GetComponent<Rigidbody>().AddForce(normal * 3.0f, ForceMode.Impulse);
>>>>>>> 066dfb5d008b6fcf9ea5602eb0236c823ee52e62
>>>>>>> Stashed changes
    }
}
