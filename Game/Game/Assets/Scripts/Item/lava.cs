using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lava : MonoBehaviour
{
    public static bool inLava = false;
    public GameObject Player;
    public Camera cam;
    public GameObject GotHitScreen;

    [SerializeField] private int damage;

    private void Start()
    {
        Player = GameObject.Find("Player");
        cam = Camera.main;
        GotHitScreen = GameObject.Find("GotHitScreen");
    }

    void Update()
    {
        if (GotHitScreen != null)
        {
            if (GotHitScreen.GetComponent<Image>().color.a > 0)
            {
                var color = GotHitScreen.GetComponent<Image>().color;
                color.a -= 0.01f;
                GotHitScreen.GetComponent<Image>().color = color;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StartCoroutine("countTime", 1);
        }
    }

    IEnumerator countTime(float delayTime)
    {
        gotHurt();
        yield return new WaitForSeconds(delayTime);
        StartCoroutine("countTime", 1);

    }

    void gotHurt()
    {
        Player.transform.GetComponent<StatusController>().DecreaseHP(damage);
        cam.GetComponent<CameraShake>().Shake();
        var color = GotHitScreen.GetComponent<Image>().color;
        color.a = 0.5f;
        GotHitScreen.GetComponent<Image>().color = color;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StopCoroutine("countTime");

        }
    }

}
