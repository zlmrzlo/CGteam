using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruct : MonoBehaviour
{
    GameObject boxs;
    // Start is called before the first frame update
    void Start()
    {
        DestructBox();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void DestructBox()
    {
        boxs = GetComponentInChildren<Transform>().gameObject;
    }
}
