using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    private Vector3 From;
    [SerializeField] private Vector3 Des;
    private float i = 0;

    // Start is called before the first frame update
    void Start()
    {
        From = wall.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        MoveWall();
    }

    void MoveWall()
    {
        wall.transform.localPosition = Vector3.Lerp(From, Des, i);
        i = Mathf.Min(1, i + 0.0001f);
    }
}
