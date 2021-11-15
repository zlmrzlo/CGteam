using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterWallSwitches : MonoBehaviour
{
    [SerializeField]
    public GameObject button;
    Material buttonMaterial;
    [SerializeField]
    public Wall[] wallObject;
    public GameObject leftGravityController;
    public GameObject leftDisappearBlock1;
    public GameObject leftDisappearBlock2;
    public GameObject FireTurret;
    public bool switchState = false;

    // Start is called before the first frame update
    void Start()
    {
        button = transform.Find("PressurePadInner").gameObject;
        buttonMaterial = button.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void switchOn()
    {
        print("switch on");
        button.GetComponent<AudioSource>().PlayDelayed(0.2f);
        buttonMaterial.SetColor("_EmissiveColor", new Color(0f, 170.0f, 170.0f, 0f));
        transform.GetChild(0).transform.Translate(new Vector3(0.0f, -0.05f, 0.0f));
        for (int i = 0; i < wallObject.Length; i++)
        {
            wallObject[i].hideWall();
        }
        leftGravityController.SetActive(true);
        leftDisappearBlock1.SetActive(true);
        leftDisappearBlock2.SetActive(true);
        FireTurret.SetActive(false);
    }

    public void switchOff()
    {
        print("switch off");
        button.GetComponent<AudioSource>().PlayDelayed(0.2f);
        buttonMaterial.SetColor("_EmissiveColor", new Color(255.0f, 0f, 0f, 0f));
        transform.GetChild(0).transform.Translate(new Vector3(0.0f, 0.05f, 0.0f));
        for (int i = 0; i < wallObject.Length; i++)
        {
            wallObject[i].unhideWall();
        }
        leftGravityController.SetActive(false);
        leftDisappearBlock1.SetActive(false);
        leftDisappearBlock2.SetActive(false);
        FireTurret.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.transform.name);
        print(switchState);
        if (other.transform.name == "hand.R")
        {
            switchState = !switchState;
            if (switchState)
            {
                switchOn();
            }
            else
            {
                switchOff();
            }

            other.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
