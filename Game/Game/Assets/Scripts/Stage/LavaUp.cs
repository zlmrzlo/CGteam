using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaUp : MonoBehaviour
{
    [SerializeField]
    private GameObject Lava;
    [SerializeField]
    private float velocity;
    private Vector3 end;
    [SerializeField]
    private bool isIn = false;
    // Start is called before the first frame update
    void Start()
    {
        end = Lava.transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIn && !GameManager.isPause)
        {
            Lava.transform.position = Vector3.MoveTowards(Lava.transform.position, end, velocity * Time.deltaTime);
            Debug.Log("용암 상승!");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isIn = true;
        }
    }
}

