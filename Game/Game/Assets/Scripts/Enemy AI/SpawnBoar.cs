using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoar : MonoBehaviour
{
    GameObject[] boars;
    public GameObject boar;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        boars = GameObject.FindGameObjectsWithTag("Boar");
        if (boars.Length <= 2)
        {
            Rigidbody.Instantiate(boar);
        }
    }
}
