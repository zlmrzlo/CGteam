using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    public Camera cam;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int knockbackPower;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.GetComponent<StatusController>().DecreaseHP(damage);
            Vector3 reactVec = collision.transform.position - transform.position;
            reactVec = reactVec.normalized;
            collision.transform.GetComponent<Rigidbody>().AddForce(reactVec * knockbackPower, ForceMode.Impulse);
            cam.GetComponent<CameraShake>().Shake();
        }
    }
}
