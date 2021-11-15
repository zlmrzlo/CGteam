using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour
{
    private static KeyBindManager instance;

    public static KeyBindManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeyBindManager>();
            }
            return instance;
        }
    }

    [SerializeField] private GameObject[] keybindButtons;
    [SerializeField] private GameObject baseUI;
    [SerializeField] private GameObject bgForKeybind;
    public static Dictionary<string, KeyCode> KeyBinds = new Dictionary<string, KeyCode>();
    public static Dictionary<string, KeyCode> LoadedKeyBinds = new Dictionary<string, KeyCode>();
    public string bindName;
    private Setting setting;

    // Start is called before the first frame update
    void Start()
    {
        setting = FindObjectOfType<Setting>();
        //KeyBinds = new Dictionary<string, KeyCode>();

        BindKey("UP", KeyCode.W, false);
        BindKey("DOWN", KeyCode.S, false);
        BindKey("LEFT", KeyCode.A, false);
        BindKey("RIGHT", KeyCode.D, false);
        BindKey("RUN", KeyCode.LeftShift, false);
        BindKey("CROUCH", KeyCode.LeftControl, false);

        BindKey("JUMP", KeyCode.Space, false);
        BindKey("ACT", KeyCode.E, false);
        BindKey("USE", KeyCode.Mouse0, false);

        // Load from Setting.cs
        if (LoadedKeyBinds.ContainsKey("USE"))
            foreach (string key in LoadedKeyBinds.Keys)
                BindKey(key, LoadedKeyBinds[key], false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            if (GameManager.isPause) CloseMenu();
    }
    public void LoadKey(string key, KeyCode kc)
    {
        LoadedKeyBinds[key] = kc;
    }
    public void BindKey(string key, KeyCode kc, bool saveBinds = true)
    {
        if(KeyBinds.ContainsValue(kc))
        {
            string nkey = KeyBinds.FirstOrDefault(x => x.Value == kc).Key;
            KeyBinds[nkey] = KeyCode.None;
            UpdateKeyText(nkey, KeyCode.None);
            if (saveBinds)
            {
                int nindex = Array.IndexOf(Setting.settingData.keyBindKey, nkey);
                Setting.settingData.keyBindVal[nindex] = KeyCode.None;
            }
            
        }
        KeyBinds[key] = kc;
        UpdateKeyText(key, kc);
        if (saveBinds)
        {
            int index = Array.IndexOf(Setting.settingData.keyBindKey, key);
            Setting.settingData.keyBindVal[index] = kc;
            Setting.SaveSetting();
        }

        bindName = string.Empty;
        if (bgForKeybind.activeSelf) bgForKeybind.SetActive(false);
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void KeyBindOnClick(string bindName)
    {
        this.bindName = bindName;
    }

    private void OnGUI()
    {
        if(bindName!=string.Empty)
        {
            bgForKeybind.SetActive(true);
            if (bindName == "ACT" || bindName == "USE")
                bgForKeybind.GetComponentInChildren<Text>().text = "계속하려면 할당할 키보드 혹은 마우스를 입력하십시오.\n";
            else
                bgForKeybind.GetComponentInChildren<Text>().text = "계속하려면 할당할 키보드를 입력하십시오.\n";
            bgForKeybind.GetComponentInChildren<Text>().text += "할당할 키: " + bindName;
            Event e = Event.current;
            if (e.isKey) BindKey(bindName, e.keyCode);
            else if((bindName == "ACT" || bindName == "USE") && e.isMouse)
                if(Input.GetKeyUp(KeyCode.Mouse0)) BindKey(bindName, KeyCode.Mouse0);
                else if (Input.GetKeyUp(KeyCode.Mouse1)) BindKey(bindName, KeyCode.Mouse1);
                else if (Input.GetKeyUp(KeyCode.Mouse2)) BindKey(bindName, KeyCode.Mouse2);
                else if (Input.GetKeyUp(KeyCode.Mouse3)) BindKey(bindName, KeyCode.Mouse3);
                else if (Input.GetKeyUp(KeyCode.Mouse4)) BindKey(bindName, KeyCode.Mouse4);
                else if (Input.GetKeyUp(KeyCode.Mouse5)) BindKey(bindName, KeyCode.Mouse5);
                else if (Input.GetKeyUp(KeyCode.Mouse6)) BindKey(bindName, KeyCode.Mouse6);
        }
    }

    public void CloseMenu()
    {
        baseUI.SetActive(false);
    }
}