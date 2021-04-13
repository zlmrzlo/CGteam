using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeShell : MonoBehaviour
{
    [SerializeField]
    private float throwPower;

    public GameObject bomb;
    public GameObject smoke;
    public Rigidbody rigid;

    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            Shoot();
            
        }
    }
    public void Shoot()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 shooting = ray.direction;
        shooting = shooting.normalized * throwPower;
        GetComponent<Rigidbody>().AddForce(shooting, ForceMode.Impulse);

        StartCoroutine(Explosion());
        
        
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(5f);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        bomb.SetActive(false);
        smoke.SetActive(true);
        yield return new WaitForSeconds(10f);
        smoke.SetActive(false);

    }

    //private void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.name == "Terrain")
    //    {
    //        rigid.velocity = Vector3.zero;
    //        rigid.angularVelocity = Vector3.zero;
    //        bomb.SetActive(false);
    //        smoke.SetActive(true);
    //    }
    //}
}
