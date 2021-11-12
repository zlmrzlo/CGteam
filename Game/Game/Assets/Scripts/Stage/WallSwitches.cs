using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitches : MonoBehaviour
{
    GameObject button;
    Material buttonMaterial;
    public GameObject wall;
    public bool switchState = false;

    // Start is called before the first frame update
    void Start()
    {
        button = transform.Find("PressurePadInner").gameObject;
        buttonMaterial = button.GetComponent<Renderer>().material;
        //wall = transform.parent.parent.transform.Find("Walls").transform.Find("WallsTrap (4)").gameObject;
        //print(wall.name);
    }

    private void Update()
    {
        if (switchState)
        {
            if(name == "WallSwitch (1)")
            {
                WallRotateRight();
            }
            else if (name == "WallSwitch (2)")
            {
                WallRotateForward();
            }
            else if (name == "WallSwitch (3)")
            {
                WallRotateUp();
            }
        }
    }

    void WallRotateRight()
    {
        wall.transform.Rotate(Vector3.right);
    }
    void WallRotateForward()
    {
        wall.transform.Rotate(Vector3.forward);
    }
    void WallRotateUp()
    {
        wall.transform.Rotate(Vector3.up);
    }

    public void switchOn()
    {
        //print("switch on");
        button.GetComponent<AudioSource>().PlayDelayed(0.2f);
        buttonMaterial.SetColor("_EmissiveColor", new Color(0f, 170.0f, 170.0f, 0f));
        transform.GetChild(0).transform.Translate(new Vector3(0.0f, -0.05f, 0.0f));
    }

    public void switchOff()
    {
        //print("switch off");
        button.GetComponent<AudioSource>().PlayDelayed(0.2f);
        buttonMaterial.SetColor("_EmissiveColor", new Color(255.0f, 0f, 0f, 0f));
        transform.GetChild(0).transform.Translate(new Vector3(0.0f, 0.05f, 0.0f));
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
