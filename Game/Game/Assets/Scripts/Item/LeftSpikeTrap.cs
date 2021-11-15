using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSpikeTrap : MonoBehaviour
{
    public GameObject Player;
    public Camera cam;
    

    [SerializeField]
    private int damage;

    [SerializeField]
    private int knockbackPower;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spike");
    }

    IEnumerator Spike()
    {
        while (true)
        {
            transform.Translate(0, 0, 0.9f);
            yield return new WaitForSeconds(2f);
            transform.Translate(0, 0, -0.9f);
            yield return new WaitForSeconds(3f);
        }
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
