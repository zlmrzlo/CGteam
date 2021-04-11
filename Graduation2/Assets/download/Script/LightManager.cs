using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField]
    private Mesh[] meshes;
    private MeshFilter mesh;
    public bool lightOn;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        lightOn = false;
        isOpen = false;
        mesh = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            changeLight();
        }
        else
        {
            mesh.sharedMesh = meshes[1];

        }
    }
    private void changeLight()
    {
        if(lightOn)
        {
            mesh.sharedMesh = meshes[1];
        }
        else
        {
            mesh.sharedMesh = meshes[0];
        }
    }
}
