using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject Player;
    // public Camera cam;


    [SerializeField]
    private int damage;

    [SerializeField]
    private int knockbackPower;

    [SerializeField]
    private float DelayTime = 0f;

    [SerializeField]
    private float UpTime = 0f;

    [SerializeField]
    private float DownTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spike");
        // cam = Camera.main;
    }

    IEnumerator Spike()
    {
        yield return new WaitForSeconds(DelayTime);

        while (true)
        {
            transform.localPosition += Vector3.up * 0.9f;
            yield return new WaitForSeconds(UpTime);
            transform.localPosition -= Vector3.up * 0.9f;
            //transform.position -= new Vector3(0, 0.9f, 0);
            yield return new WaitForSeconds(DownTime);
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
            // cam.GetComponent<CameraShake>().Shake();

        }
    }
}
