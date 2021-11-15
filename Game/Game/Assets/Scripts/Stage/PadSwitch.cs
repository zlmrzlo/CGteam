using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadSwitch : SwitchManager
{
    [SerializeField]
    private GameObject pad;
    private Material padMaterial;
    private AudioSource audio;

    [SerializeField]
    private float weight;

    // Start is called before the first frame update
    void Start()
    {
        this.audio = this.transform.GetComponent<AudioSource>();
        turnOn = false;
        isOpen = false;
        padMaterial = pad.GetComponent<Renderer>().material;
        
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    protected override void turnOnSwitch()
    {
        turnOn = true;
        
    }
    private void checkWeight(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            return;
        }
        if (other.CompareTag("BulletLava"))
        {
            return;
        }
        GameObject otherobject = other.gameObject;
        Rigidbody rigid = otherobject.GetComponent<Rigidbody>();
        if (rigid.mass > weight)
        {

            switchOn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isOpen == true)
            return;
        checkWeight(other);
        if(turnOn)
            this.audio.Play();
    }
    private void OnTriggerStay(Collider other)
    {
        if (isOpen == true)
            return;
        checkWeight(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isOpen)
        {
            switchOff();
        }
        else
        {
            light.isOpen = true;
        }
    }
    private void switchOn()
    {
        padMaterial.SetColor("_EmissiveColor", new Color(0f,170.0f,170.0f,0f));
        turnOnSwitch();
        light.lightOn = true;

    }
    private void switchOff()
    {
        padMaterial.SetColor("_EmissiveColor", new Color(255.0f, 0f, 0f,0f));
        light.lightOn = false;
        turnOn = false;
    }
    protected override IEnumerator SwitchCoroutine()
    {
        throw new System.NotImplementedException();
    }
}
