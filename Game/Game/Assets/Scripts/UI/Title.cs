using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "SampleScene";
    public static Title instance;
    private SaveAndLoad theSNL;
    [SerializeField] private GameObject SettingUI;
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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        Destroy(this.gameObject);
    }
    public void CallSetting()
    {
        this.gameObject.SetActive(false);
        SettingUI.SetActive(true);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
