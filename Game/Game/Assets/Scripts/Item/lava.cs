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

    // Update is called once per frame
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
<<<<<<< Updated upstream

<<<<<<< HEAD
    private void OnTriggerExit(Collider other)
=======
    private void OnCollisionEnter(Collision collision)
>>>>>>> Stashed changes
=======
    private void OnTriggerEnter(Collider other)
>>>>>>> 08f71a5a3f9e42b62e04ccfdee109215be372b61
    {
        if (collision.transform.tag == "Player")
        {
            StartCoroutine("countTime", 1);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            StopCoroutine("countTime");

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
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
<<<<<<< HEAD
<<<<<<< Updated upstream
        if (inLava == true)
            statusController.currentHp -= Time.deltaTime * damage;
=======
        
>>>>>>> Stashed changes
=======
        if (other.transform.tag == "Player")
        {
            StopCoroutine("countTime");

        }
>>>>>>> 08f71a5a3f9e42b62e04ccfdee109215be372b61
    }
}
