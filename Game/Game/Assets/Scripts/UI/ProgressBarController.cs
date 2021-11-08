using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private GameObject resetPosObj;
    //private PauseMenu pauseMenu;

    private void Start()
    {
        //pauseMenu = FindObjectOfType<PauseMenu>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (resetPosObj)
            {
                RestartInfo.resetPosition = resetPosObj.transform.position;
                RestartInfo.resetRotation = resetPosObj.transform.eulerAngles;
                Destroy(resetPosObj);
            }
        }
    }
}
