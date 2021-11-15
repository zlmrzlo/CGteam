using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    bool moveSwitch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // moveSwitch = false은 밀기
        // moveSwitch = true은 당기기
        //print(this.transform.position);
        if(this.transform.position.z <= -3.5f)
        {
            moveSwitch = false;
        }
        if(this.transform.position.z >= 82.5f)
        {
            moveSwitch = true;
        }

        if (!moveSwitch)
        {
            PushFloor();
        }
        else
        {
            PullFloor();
        }
    }

    void PushFloor()
    {
        this.transform.Translate(0, 0.05f, 0);
    }

    void PullFloor()
    {
        this.transform.Translate(0, -0.05f, 0);
    }

}
