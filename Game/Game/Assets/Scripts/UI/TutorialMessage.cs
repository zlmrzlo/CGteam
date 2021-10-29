using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour
{
    [SerializeField] private GameObject messageParent;
    [SerializeField] private Text messageText;
    [SerializeField] private string message;

    private Vector3 outPos = new Vector3(772, 0, 0);
    private Vector3 inPos = new Vector3(508, 0, 0);
    private bool isIn = false;
    private bool isOut = false;
    private float t = 0f;

    private void Update()
    {
        if (isIn)
        {
            Vector3 velo = Vector3.zero;
            messageParent.transform.localPosition = Vector3.Lerp(messageParent.transform.localPosition, inPos, Time.deltaTime * 5);
        }
        if (isOut)
        {
            messageParent.transform.localPosition = Vector3.Lerp(inPos, outPos, t*t*3);
            t += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !messageParent.activeSelf)
        {
            isIn = true;
            messageParent.SetActive(true);
            messageText.text = message;
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        isIn = false;
        isOut = true;
        StartCoroutine("WaitForAnotherSec");
    }

    IEnumerator WaitForAnotherSec()
    {
        yield return new WaitForSeconds(1);
        t = 0f;
        messageParent.transform.localPosition = outPos;
        messageParent.SetActive(false);
        Destroy(gameObject);
    }
}
