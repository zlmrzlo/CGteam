using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour
{
    [SerializeField] private GameObject messageParent;
    [SerializeField] private Text messageText;
    [SerializeField] private string message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            messageParent.SetActive(true);
            messageText.text = message;
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        messageParent.SetActive(false);
        Destroy(gameObject);
    }
}
