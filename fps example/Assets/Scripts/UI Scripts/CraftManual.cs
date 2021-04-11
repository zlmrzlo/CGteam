using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Craft
{
    public string craftName;
    public GameObject prefab;
    public GameObject previewPrefab;
}

public class CraftManual : MonoBehaviour
{
    private bool isActivated = false;
    private bool isPreviewActivated = false;
    [SerializeField] private GameObject baseUI;

    [SerializeField] private Craft[] craftFire;
    private GameObject preview;
    private GameObject prefab;
    [SerializeField] private Transform player;

    private RaycastHit hitInfo;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float range;

    public void SlotClick(int _slotNumber)
    {
        preview = Instantiate(craftFire[_slotNumber].previewPrefab, player.position + player.forward, Quaternion.identity);
        prefab = craftFire[_slotNumber].prefab;
        isPreviewActivated = true;
        baseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
        {
            GameManager.isOpenCraftManual = true;
            Window();
        }
        if (isPreviewActivated)
        {
            GameManager.isOpenCraftManual = false;
            PreviewPositionUpdate();
        }
        if (Input.GetButtonDown("Fire1"))
            Build();
        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
    }

    private void Build()
    {
        if (isPreviewActivated && preview.GetComponent<PreviewObject>().isBuildable()) 
        {
            Instantiate(prefab, hitInfo.point, Quaternion.identity);
            Destroy(preview);
            isActivated = false;
            isPreviewActivated = false;
            preview = null;
            prefab = null;
        }
    }

    private void PreviewPositionUpdate()
    {
        if (Physics.Raycast(player.position, player.forward, out hitInfo, range, layerMask)) 
        {
            if (hitInfo.transform != null) 
            {
                Vector3 _location = hitInfo.point;
                preview.transform.position = _location;
            }
        }
    }

    private void Cancel()
    {
        if (isPreviewActivated)
            Destroy(preview);
        isActivated = false;
        isPreviewActivated = false;
        preview = null;
        prefab = null;
        baseUI.SetActive(false);
    }

    private void Window()
    {
        if (!isActivated)
            OpenWindow();
        else
            CloseWindow();
    }

    private void OpenWindow()
    {
        isActivated = true;
        baseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        isActivated = false;
        baseUI.SetActive(false);
    }
}
