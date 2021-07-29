using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    private SwitchManager[] switchs;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private float doorSpeed;
    private Vector3 doorFinishPos;

    //private bool openDoor = false;
    private int turnOnSwitch;

    void Start()
    {
        turnOnSwitch = 0;
        doorFinishPos = new Vector3(door.transform.position.x, door.transform.position.y - 3.5f, door.transform.position.z);
    }

    void Update()
    {
        if (!GameManager.isPause) CheckSwitch();
    }

    private void CheckSwitch()
    {
        for(int i = 0; i<switchs.Length; i++)
        {
            if (switchs[i].turnOn == true)
            {
                turnOnSwitch++;
            }
        }
        if(turnOnSwitch == switchs.Length)
        {
            OpenDoor();
        }
        else
        {
            turnOnSwitch = 0;
        }
    }
    private void OpenDoor()
    {
        for (int i = 0; i < switchs.Length; i++)
        {
            switchs[i].isOpen = true;
        }
        door.transform.position = Vector3.MoveTowards(door.transform.position, doorFinishPos, doorSpeed);

    }
}
