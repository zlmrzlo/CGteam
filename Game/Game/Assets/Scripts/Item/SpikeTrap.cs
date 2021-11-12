using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject Player;
    public Camera cam;
    public float spikeDistance = 0.9f;


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
        // new Quaternion(-0.3f, -0.6f, 0.7f, -0.3f)
        // spikeTrap이 z방향으로 180도 돌아갔을 때의 쿼터니언값
        //print(this.GetComponentInParent<Transform>().rotation == new Quaternion(-0.3f, -0.6f, 0.7f, -0.3f));
        if(this.GetComponentInParent<Transform>().rotation == new Quaternion(-0.3f, -0.6f, 0.7f, -0.3f))
        {
            spikeDistance *= -1;
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        StartCoroutine("Spike");
    }

    IEnumerator Spike()
    {
        yield return new WaitForSeconds(DelayTime);

        while (true)
        {
            transform.position += new Vector3(0, spikeDistance, 0);
            yield return new WaitForSeconds(UpTime);
            transform.position -= new Vector3(0, spikeDistance, 0);
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
            cam.GetComponent<CameraShake>().Shake();

        }
    }
}
