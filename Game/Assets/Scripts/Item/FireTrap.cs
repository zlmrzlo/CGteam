using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    public GameObject fire;
    public GameObject collider;

    [SerializeField]
    private float startTime = 0f;

    [SerializeField]
    private float delayTime1 = 3f;

    [SerializeField]
    private float delayTime2 = 3f;

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
            yield return new WaitForSeconds(delayTime1);
            fire.SetActive(false);
            collider.SetActive(false);
            yield return new WaitForSeconds(delayTime2);
            fire.SetActive(true);
            collider.SetActive(true);
        }
    }
}
