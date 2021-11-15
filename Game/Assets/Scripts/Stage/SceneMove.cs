using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public Transform stage3_UI;
    public Transform stage3_Item;
    public Transform stage3_UI_Out;
    public Transform stage3_Item_Out;
    public Transform stage3_Start;
    public Transform stage3_Stage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            if (this.transform.parent.parent.name == "Door (1)")
            {
                other.transform.position = stage3_UI.position;
            }
            else if(this.transform.parent.parent.name == "Door (2)")
            {
                other.transform.position = stage3_Item.position;
            }
            else if (this.transform.parent.parent.name == "ItemStageDoor")
            {
                other.transform.position = stage3_Item_Out.position;
            }
            else if (this.transform.parent.parent.name == "UIStageDoor")
            {
                other.transform.position = stage3_UI_Out.position;
            }
            else if (this.transform.parent.parent.name == "24Door")
            {
                other.transform.position = stage3_Start.position;
            }
            else if (this.transform.parent.parent.name == "ToonStageDoor")
            {
                other.transform.position = stage3_Stage.position;
            }
        }
    }
}
