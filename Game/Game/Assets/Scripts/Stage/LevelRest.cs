using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRest : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;

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
        if (other.CompareTag("Player") && this.name == "Moving platform (3)")
        {
            GetComponentInParent<Level1>().lava55.SetActive(false);
        }

        if (other.CompareTag("Player") && this.name == "Moving platform (5)")
        {
            print("hit");
            GetComponent<LevelRest>().level2.GetComponent<Level2>().lava21.SetActive(false);
            level2.SetActive(true);
        }


        if (other.CompareTag("Player") && this.name == "UpGravity")
        {
            GetComponentInParent<Level2>().lava11.SetActive(false);
            GetComponentInParent<Level2>().lava19.SetActive(false);
            level1.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print(this.tag);
        if (collision.transform.tag == "Player" && this.tag == "Lava22_28")
        {
            print("hit");
            //print(level2.GetComponent<Level2>().lava22);
            Level2 lavaTarget = level2.GetComponent<Level2>();
            foreach (GameObject lava in lavaTarget.lava22_28_16)
            {
                lava.SetActive(false);
            }
            //level2.GetComponent<Level2>().lava22.SetActive(false);
            //level2.GetComponent<Level2>().lava23.SetActive(false);
            //level2.GetComponent<Level2>().lava24.SetActive(false);
            //level2.GetComponent<Level2>().lava25.SetActive(false);
            //level2.GetComponent<Level2>().lava26.SetActive(false);
            //level2.GetComponent<Level2>().lava27.SetActive(false);
            //level2.GetComponent<Level2>().lava28.SetActive(false);
        }
    }
}
