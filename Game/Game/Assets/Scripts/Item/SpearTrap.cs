using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    public GameObject Player;
    public GameObject Trap;
    public GameObject Spear;
    private float Dist;

    [SerializeField]
    float distance;

    void Update()
    {
        Dist = Vector3.Distance(Player.transform.position, Trap.transform.position);
        if (Dist <= distance)
            Fire();
    }

    void Fire()
    {
        for (int i = 0; i < 5; i++)
        {
            Spear.transform.Translate(new Vector3(0, -0.1f, 0));
            i++;
        }
    }
}
