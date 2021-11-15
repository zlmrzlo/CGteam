using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    private bool isEntered = false;
    [SerializeField] private GameObject resetPosObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isEntered)
        {
            isEntered = true;
            StatusController st = other.GetComponent<StatusController>();
            st.IncreaseHP(1000);
        }
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
