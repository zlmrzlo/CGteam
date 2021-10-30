using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour
{
    public static bool inLava = false;
    private StatusController statusController;

    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        statusController = FindObjectOfType<PlayerController>().GetComponent<StatusController>();
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
            statusController.currentHp -= Time.deltaTime * damage;
    }
}
