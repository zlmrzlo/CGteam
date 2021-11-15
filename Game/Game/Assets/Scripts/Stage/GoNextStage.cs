using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNextStage : MonoBehaviour
{
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
        var obj = other.gameObject;
        obj.transform.position = transform.GetChild(0).position;
    }
}
