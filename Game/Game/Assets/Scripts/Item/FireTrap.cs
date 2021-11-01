using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    public GameObject fire;
    public GameObject collider;

    [SerializeField]
    private float startTime = 2f;

    [SerializeField]
    private float delaytTime1 = 2f;

    [SerializeField]
    private float delaytTime2 = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(startTime);

        while (true)
        {
            yield return new WaitForSeconds(delaytTime1);
            fire.SetActive(false);
            collider.SetActive(false);
            yield return new WaitForSeconds(delaytTime2);
            fire.SetActive(true);
            collider.SetActive(true);
        }
    }
}
