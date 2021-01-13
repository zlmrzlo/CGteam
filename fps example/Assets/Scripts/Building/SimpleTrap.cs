using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrap : MonoBehaviour
{
    private Rigidbody[] rigid;
    [SerializeField] private GameObject meat;
    [SerializeField] private int damage;
    private bool isActivated = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip soundActivated;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponentsInChildren<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isActivated)
        {
            if (other.transform.tag != "Untagged")
            {
                isActivated = true;
                audioSource.clip = soundActivated;
                audioSource.Play();
                Destroy(meat);

                for (int i = 0; i < rigid.Length; i++)
                {
                    rigid[i].useGravity = true;
                    rigid[i].isKinematic = false;
                }
                if (other.transform.name == "Player") 
                {
                    other.transform.GetComponent<StatusController>().DecreaseHP(damage);
                }
            }
        }
    }
}
