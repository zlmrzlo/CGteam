using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    GameObject button;
    Material buttonMaterial;

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
        buttonMaterial.SetColor("_EmissiveColor", new Color(0f, 170.0f, 170.0f, 0f));
    }

    public void switchOff()
    {
        buttonMaterial.SetColor("_EmissiveColor", new Color(255.0f, 0f, 0f, 0f));
    }
}
