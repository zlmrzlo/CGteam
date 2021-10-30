using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftStage : MonoBehaviour
{
    public GameObject blocks;
    public GameObject leftWall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            blocks.SetActive(false);
            leftWall.SetActive(true);
        }
    }
}
