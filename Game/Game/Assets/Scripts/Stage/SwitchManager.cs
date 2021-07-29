using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchManager : MonoBehaviour
{
    public bool turnOn;
    public bool isOpen;
    [SerializeField]
    protected new LightManager light;
    [SerializeField]
    protected float turnOnTime;

 
    // Start is called before the first frame update

    protected abstract void turnOnSwitch();
    protected abstract IEnumerator SwitchCoroutine();
}
