using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public string sceneName = "Stage";
    public static Title instance;
    private SaveAndLoad theSNL;
    [SerializeField] private GameObject SettingUI;
    private GameObject[] buttons;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("TitleButton");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if(!SettingUI.activeSelf)
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].SetActive(true);
        }
    }

    public void ClickStart()
    {
        Debug.Log("로딩");
        SceneManager.LoadScene(sceneName);
        Destroy(this.gameObject);
    }

    public void ClickLoad()
    {
        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
            yield return null;
        theSNL = FindObjectOfType<SaveAndLoad>();
        theSNL.LoadData();
        Destroy(gameObject);
    }
    public void CallSetting()
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].SetActive(false);
        //this.gameObject.SetActive(false);
        SettingUI.SetActive(true);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
