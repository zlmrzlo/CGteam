using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour
{
    public static bool inLava = false;

    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onFire();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GetLava(other);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetOutLava(other);
        }
    }

    private void GetLava(Collider _player)
    {
        inLava = true;

    }

    private void GetOutLava(Collider _player)
    {
        inLava = false;
    }

    void onFire()
    {
        if (inLava == true)
            StatusController.currentHp -= Time.deltaTime * damage;
    }
}
