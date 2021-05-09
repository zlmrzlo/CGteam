using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public Transform original;
    public Slot inventorySlot;
    public Transform thisSlot;

    public Image itemImage;

    [SerializeField] private Text textCount;
    [SerializeField] private GameObject countImage;

    public GameObject InstObject;
    public int scrollPosition;

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckInventory();
        Selected();
        if(!GameManager.isPause)
        {
            ChangeSlot();
            CheckUsed();
        }
    }

    private void CheckInventory()
    {
        if (inventorySlot.countImage.activeSelf)
        {
            itemImage.sprite = inventorySlot.item.itemImage;
            countImage.SetActive(true);
            textCount.text = inventorySlot.itemCount.ToString();
            SetColor(1);
        }
        else
        {
            itemImage.sprite = null;
            textCount.text = "0";
            countImage.SetActive(false);
            SetColor(0);
        }
    }

    private void ChangeSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            scrollPosition = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            scrollPosition = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            scrollPosition = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            scrollPosition = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            scrollPosition = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            scrollPosition = 5;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            scrollPosition = 6;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            scrollPosition = 7;
        if (Input.mouseScrollDelta.y <= -1)
        {
            scrollPosition++;
            if (scrollPosition >= 8) scrollPosition = 0;
        }
        if (Input.mouseScrollDelta.y >= 1)
        {
            scrollPosition--;
            if (scrollPosition <= -1) scrollPosition = 7;
        }
    }
    private void Selected()
    {
        if (thisSlot.name == "HotbarSlot" + (scrollPosition + 1))
            thisSlot.GetComponent<Image>().color = Color.white;
        else
            thisSlot.GetComponent<Image>().color = original.GetComponent<Image>().color;
    }

    public void CheckUsed()
    {
        if (Input.GetKeyDown(KeyCode.F))
            if (thisSlot.name == "HotbarSlot" + (scrollPosition + 1) && inventorySlot.item != null)
                inventorySlot.UseItem();
    }
}
