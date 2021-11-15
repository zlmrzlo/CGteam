using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour
{
    public GameObject[] walls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.name == "WallsTrap (1)")
        {
            RotateWallsForward();
        }
        //else if(transform.name == "WallsTrap (4)")
        //{
        //    RotateWallsRight();
        //}
    }

    void RotateWallsForward()
    {
        foreach (GameObject wall in walls)
        {
            wall.GetComponent<Transform>().Rotate(Vector3.forward);
        }
    }
    //void RotateWallsRight()
    //{
    //    foreach (GameObject wall in walls)
    //    {
    //        wall.GetComponent<Transform>().Rotate(Vector3.right, 0.1f);
    //    }
    //}
}
