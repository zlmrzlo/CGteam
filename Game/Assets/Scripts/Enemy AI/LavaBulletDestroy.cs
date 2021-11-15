using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBulletDestroy : MonoBehaviour
{
    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
