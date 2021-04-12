using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEffect : MonoBehaviour
{
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(other.transform.tag == "Player")
        {
            print("Player");
        }
    }
}
