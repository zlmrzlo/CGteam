using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
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
            transform.position += new Vector3(0, 0.9f, 0);
            yield return new WaitForSeconds(2f); 
            transform.position -= new Vector3(0, 0.9f, 0);
            yield return new WaitForSeconds(1f);
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
