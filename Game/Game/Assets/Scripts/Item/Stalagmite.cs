using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    public GameObject Player;
    public GameObject Stone;
    public Camera cam;
    private float Dist;

    [SerializeField]
    private float distance;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int knockbackPower;


    void Update()
    {
        Dist = Vector3.Distance(Player.transform.position, Stone.transform.position);
        Fall();
    }

    void Fall()
    {
        if (Dist <= distance)
            Stone.GetComponent<Rigidbody>().useGravity = true;
    }

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
