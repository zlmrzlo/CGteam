using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private Light wallLight;
    private Vector3 From;
    [SerializeField] private Vector3 Des;
    private float i = 0;
    private bool activated = false;
    public AudioClip start;
    public AudioClip drag;
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;

    // Start is called before the first frame update
    void Start()
    {
        From = wall.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPause && activated)
            MoveWall();
        else
            audioSource2.Stop();
    }

    void MoveWall()
    {
        audioSource2.loop = true;
        audioSource2.clip = drag;
        if (!audioSource2.isPlaying)
            audioSource2.Play();
        if (Mathf.Abs(wall.transform.localPosition.x - Des.x) > 0.0001f)
        {
            wall.transform.localPosition = Vector3.Lerp(From, Des, i);
            i = Mathf.Min(1, i + 0.00005f);
        }
        else
            audioSource2.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            activated = true;
            wallLight.color = Color.red;
            audioSource1.clip = start;
            audioSource1.Play();
        }
    }
}
