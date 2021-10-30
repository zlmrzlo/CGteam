using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string sceneName = "Stage";
    [SerializeField] private GameObject BaseUI;
    [SerializeField] private GameObject SettingUI;
    [SerializeField] private SaveAndLoad theSNL;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        if(RestartInfo.isRestart)
        {
            player = FindObjectOfType<PlayerController>();
            player.transform.position = RestartInfo.resetPosition;
            player.transform.eulerAngles = RestartInfo.resetRotation;
            RestartInfo.isRestart = false;
        }
    }
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
    public void CloseMenu()
    {
        GameManager.isPause = false;
        BaseUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ClickRestart()
    {
        if (lava.inLava) lava.inLava = false;
        RestartInfo.isRestart = true;
        GameManager.isPause = false;
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void CallSetting()
    {
        SettingUI.SetActive(true);
    }
    public void ClickSave()
    {
        theSNL.SaveData();
    }
    public void ClickExit()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
