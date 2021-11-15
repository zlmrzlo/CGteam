using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject lava99;
    public GameObject lava55;
    public GameObject lava11;
    public GameObject switch1;
    bool turnOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSwitch();
    }

    void CheckSwitch()
    {
        if(switch1.GetComponentInChildren<PadSwitch>().turnOn == true)
        {
            DeleteLava();
        }
    }

    void DeleteLava()
    {
        lava99.SetActive(false);
    }
}
