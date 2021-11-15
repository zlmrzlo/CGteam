using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    GameObject button;
    Material buttonMaterial;
    [SerializeField]
    public Wall[] wallObject;
    public GameObject leftGravityController;
    public GameObject leftDisappearBlock1;
    public GameObject leftDisappearBlock2;
    public GameObject FireTurret;

    // Start is called before the first frame update
    void Start()
    {
        buttonMaterial = button.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void switchOn()
    {
        print("switch on");
        buttonMaterial.SetColor("_EmissiveColor", new Color(0f, 170.0f, 170.0f, 0f));
        for (int i = 0; i < wallObject.Length; i++)
        {
            wallObject[i].hideWall();
            leftGravityController.SetActive(true);
            leftDisappearBlock1.SetActive(true);
            leftDisappearBlock2.SetActive(true);
            FireTurret.SetActive(false);
        }
    }

    public void switchOff()
    {
        print("switch off");
        buttonMaterial.SetColor("_EmissiveColor", new Color(255.0f, 0f, 0f, 0f));
        for (int i = 0; i < wallObject.Length; i++)
        {
            wallObject[i].unhideWall();
        }
        leftGravityController.SetActive(false);
        leftDisappearBlock1.SetActive(false);
        leftDisappearBlock2.SetActive(false);
        FireTurret.SetActive(true);
    }
}
