using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    [SerializeField]
    GameObject potion;

    Animator animator;

    AudioSource eating_Sound;

    bool is_Eat = false;

    StatusController statusController;

    // Start is called before the first frame update
    void Start()
    {
        statusController = GetComponentInParent<StatusController>();
        eating_Sound = potion.GetComponent<AudioSource>();
        if(eating_Sound != null)
        {
            //Debug.Log("eating sound");
        }

        animator = GetComponentInChildren<Animator>();
        if(animator != null)
        {
            //Debug.Log("hit animator"); 
        }
        potion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TryEat();
    }

    public void TryEat()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            potion.SetActive(true);
            animator.SetBool("Eat", true);
        }
    }

    public void EatingSound()
    {
        eating_Sound.Play();
        if (statusController.currentHp < 100)
        {
            //Debug.Log(statusController.currentHp);
            statusController.IncreaseHP(20);
        }
    }

    public void EatStop()
    {
        animator.SetBool("Eat", false); 
        potion.SetActive(false);
    }
}
