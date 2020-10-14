using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenu;

    private void Start()
    {
        inGameMenu.SetActive(false);
    }
    public void Resume()
    {
        inGameMenu.SetActive(false);
    }

    public void BackToMM(string MMSceneName)
    {
        SceneManager.LoadSceneAsync(MMSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        inGameMenu.SetActive(true);
    }

}
