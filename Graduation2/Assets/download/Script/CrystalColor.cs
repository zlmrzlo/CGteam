using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalColor : MonoBehaviour
{
    
    private GameObject crystal;
    [SerializeField]
    private Transform[] crystals;

    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        crystal = gameObject;
        position = crystal.transform.position;
        rotation = crystal.transform.rotation;
        scale = crystal.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var newCrystal = Instantiate(crystals[0], position, rotation);
            newCrystal.localScale = scale;
            Destroy(crystal);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var newCrystal = Instantiate(crystals[1], position, rotation);
            newCrystal.localScale = scale;
            Destroy(crystal);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var newCrystal = Instantiate(crystals[2], position, rotation);
            newCrystal.localScale = scale;
            Destroy(crystal);
        }
    }
}
