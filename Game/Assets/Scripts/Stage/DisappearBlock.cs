using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearBlock : MonoBehaviour
{
    [SerializeField]
    private float Disappeartime = 3.0f, Regentime = 5.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke("deactive", Disappeartime);
            Invoke("active", Regentime + Disappeartime);
        }
    }
    private void deactive()
    {
        gameObject.SetActive(false);
    }
    private void active()
    {
        gameObject.SetActive(true);
    }
}
