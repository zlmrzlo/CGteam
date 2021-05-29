using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public float playerHP;
    public float playerMP;
    public Vector3 playerPos;
    public Vector3 playerRot;
    public gravityDirection playerGDir;
    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
    public List<Vector3> boxPos = new List<Vector3>();
    public List<Vector3> boxRot = new List<Vector3>();
    public List<gravityDirection> boxGDir = new List<gravityDirection>();
}

public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";
    private GameObject player;
    private GameObject[] boxes;

    private Inventory inven;
    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveData()
    {
        player = GameObject.FindWithTag("Player");
        Object playerObj = player.GetComponent<Object>();
        inven = FindObjectOfType<Inventory>();
        boxes = GameObject.FindGameObjectsWithTag("Object");

        saveData.playerHP = StatusController.currentHp;
        saveData.playerMP = StatusController.currentMp;
        saveData.playerPos = player.transform.position;
        saveData.playerRot = player.transform.eulerAngles;
        saveData.playerGDir = playerObj.gDirection;
        Slot[] slots = inven.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null) 
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }
        for (int i = 0; i < boxes.Length; i++)
        {
            saveData.boxPos.Add(boxes[i].transform.position);
            saveData.boxRot.Add(boxes[i].transform.eulerAngles);
            saveData.boxGDir.Add(boxes[i].GetComponent<Object>().gDirection);
        }

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
    }
    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);
            player = GameObject.FindWithTag("Player");
            Object playerObj = player.GetComponent<Object>();
            inven = FindObjectOfType<Inventory>();
            boxes = GameObject.FindGameObjectsWithTag("Object");

            StatusController.currentHp = saveData.playerHP;
            StatusController.currentMp = saveData.playerMP;
            player.transform.position = saveData.playerPos;
            player.transform.eulerAngles = saveData.playerRot;
            playerObj.gDirection = saveData.playerGDir;
            playerObj.changeGravity(saveData.playerGDir);

            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                inven.LoadToInventory(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);
            }
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].transform.position = saveData.boxPos[i];
                boxes[i].transform.eulerAngles = saveData.boxRot[i];
                boxes[i].GetComponent<Object>().gDirection = saveData.boxGDir[i];
            }
        }
        else
            Debug.Log("세이브 파일이 없습니다.");
    }
}
