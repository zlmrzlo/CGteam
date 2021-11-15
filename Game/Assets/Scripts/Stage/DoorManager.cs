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
    public float upPos = -3.5f;
    public float forwardPos = 0.0f;
    public GameObject barrier;


    //private bool openDoor = false;
    private int turnOnSwitch;

    void Start()
    {
        turnOnSwitch = 0;
        doorFinishPos = new Vector3(door.transform.position.x, door.transform.position.y + upPos, door.transform.position.z + forwardPos);
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
        if(barrier != null)
        {
            barrier.SetActive(true);
        }
    }
}
