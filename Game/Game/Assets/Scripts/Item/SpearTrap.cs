using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    public GameObject Player;
    public GameObject Trap;
    public GameObject Spear;
    // public Camera cam;
    private float Dist;
    bool alreadyAttacked;
    public float bulletPower = 1.0f;
    public float timeBetweenAttacks = 0.5f;

    [SerializeField]
    float distance;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int knockbackPower;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        // cam = Camera.main;
    }

    void Update()
    {
        Dist = Vector3.Distance(Player.transform.position, Spear.transform.position);
        if (Dist <= distance)
            Fire();

    }

    void Fire()
    {
        for (int i = 0; i < 10; i++)
        {
            Spear.transform.Translate(new Vector3(0, -0.1f, 0));
            i++;
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.GetComponent<StatusController>().DecreaseHP(damage);
            Vector3 reactVec = collision.transform.position - transform.position;
            reactVec = reactVec.normalized;
            collision.transform.GetComponent<Rigidbody>().AddForce(reactVec * knockbackPower, ForceMode.Impulse);
            // cam.GetComponent<CameraShake>().Shake();

        }
    }
}
