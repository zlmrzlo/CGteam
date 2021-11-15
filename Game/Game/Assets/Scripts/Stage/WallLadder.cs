using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLadder : MonoBehaviour
{
    Material wallMaterial;

    // Start is called before the first frame update
    void Start()
    {
        LadderRandom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LadderRandom()
    {
        for(int i = 3; i < transform.childCount - 1; i += 2)
        {
            //print(transform.GetChild(i).name);
            //print(Random.Range(1, transform.childCount - 1));
            transform.GetChild(Random.Range(i, i + 2)).GetComponent<BoxCollider>().enabled = false;
            wallMaterial = transform.GetChild(Random.Range(i, i + 2)).GetComponent<Renderer>().material;
            wallMaterial.color = new Color(1, 1, 1, Random.Range(2, 7) * 30.0f/255.0f);
            //Random.Range(1, transform.childCount - 1);
        }
    }
}
