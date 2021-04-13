using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject BaseUI;
    [SerializeField] private GameObject SettingUI;
    [SerializeField] private SaveAndLoad theSNL;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager.isPause)
                CallMenu();
            else
                CloseMenu();
        }
    }
    private void CallMenu()
    {
        GameManager.isPause = true;
        BaseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CallSetting()
    {
        SettingUI.SetActive(true);
    }
    public void CloseMenu()
    {
        GameManager.isPause = false;
        BaseUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ClickSave()
    {
        theSNL.SaveData();
    }
    public void ClickExit()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("New Scene");
    }
}
