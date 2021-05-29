using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour 
{
    public string sceneName = "Stage";

    public void ClickRestart()
    {
        if (lava.inLava) lava.inLava = false;
        RestartInfo.isRestart = true;
        //string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.canPlayerMove = true;
    }

    /*public void ClickLoad()
    {
        if (lava.inLava) lava.inLava = false;
        //StartCoroutine(LoadCoroutine());


        SceneManager.LoadScene(sceneName);
        SaveAndLoad theSNL = FindObjectOfType<SaveAndLoad>();
        theSNL.LoadData();
        GameManager.isPause = false;
        Time.timeScale = 1f;
    }

    IEnumerator LoadCoroutine()
    {
        //string sceneName = SceneManager.GetActiveScene().name;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
            yield return null;
        SaveAndLoad theSNL = FindObjectOfType<SaveAndLoad>();
        theSNL.LoadData();
        GameManager.isPause = false;
        Destroy(gameObject);
    }*/

    public void ClickExit()
    {
        if (lava.inLava) lava.inLava = false;
        SceneManager.LoadScene("Title");
    }
}
