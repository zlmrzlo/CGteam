using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public void hideWall()
    {
        this.gameObject.SetActive(false);
    }

    public void unhideWall()
    {
        this.gameObject.SetActive(true);
    }
}
