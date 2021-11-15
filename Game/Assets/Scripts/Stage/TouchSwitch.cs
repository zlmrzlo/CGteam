using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSwitch : SwitchManager
{
    [SerializeField]
    private Mesh[] meshes;
    private MeshFilter mesh;
    // Start is called before the first frame update
    void Start()
    {
        turnOn = false;
        isOpen = false;
        mesh = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        turnOnSwitch();
    }
    protected override IEnumerator SwitchCoroutine()
    {
        turnOn = true;
        light.lightOn = true;
        mesh.sharedMesh = meshes[1];
        yield return new WaitForSeconds(turnOnTime);
        if (isOpen == true)
        {
            turnOn = true;
            light.lightOn = true;
            mesh.sharedMesh = meshes[1];
            light.isOpen = true;
            StopAllCoroutines();
        }
        else
        {
            turnOn = false;
            light.lightOn = false;
            mesh.sharedMesh = meshes[0];
        }
    }

    protected override void turnOnSwitch()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
             StartCoroutine(SwitchCoroutine());
        }      
    }
}
