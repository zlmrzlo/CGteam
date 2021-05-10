using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private GameObject progressObject;
    [SerializeField] private float position;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            progressObject.transform.localPosition = new Vector3(position, 0, 0);
            StartCoroutine("Blink");
        }
    }
    IEnumerator Blink()
    {
        Image image;
        image = progressObject.GetComponent<Image>();

        image.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        image.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.5f);
        image.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        image.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.5f);
        image.color = new Color(1, 1, 1, 1);
    }
}
