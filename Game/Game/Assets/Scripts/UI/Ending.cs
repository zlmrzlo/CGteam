using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] private Image whiteFadeIn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.isPause = true;
            GameObject[] uis = GameObject.FindGameObjectsWithTag("UIs");
            for (int i = 0; i < uis.Length; i++)
                uis[i].SetActive(false);
            uis = GameObject.FindGameObjectsWithTag("UIs2");
            for (int i = 0; i < uis.Length; i++)
                uis[i].SetActive(false);

            StartCoroutine("EndingCoroutine");
        }
    }
    IEnumerator EndingCoroutine()
    {
        while (whiteFadeIn.color.a < 1)
        {
            Color c = whiteFadeIn.color;
            c.a += 0.0025f;
            whiteFadeIn.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        GameManager.isPause = false;
        SceneManager.LoadScene("Title");
    }
}
