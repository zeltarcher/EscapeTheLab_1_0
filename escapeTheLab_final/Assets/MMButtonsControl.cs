using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MMButtonsControl : MonoBehaviour
{
    private GameObject ButtonContainer;
    private GameObject OptionsContainer;
    private GameObject InfoContainer;
    private GameObject MMCanvas;
    private GameObject HUDCanvas;

    void Start()
    {
        MMCanvas = GameObject.Find("Canvas-MM");
        ButtonContainer = GameObject.Find("ButtonContainer");
        OptionsContainer = GameObject.Find("OptionsContainer");
        InfoContainer = GameObject.Find("InfoContainer");
        /*if (OptionsContainer != null || InfoContainer != null || HUDCanvas != null)
            if (OptionsContainer.activeSelf == false || InfoContainer.activeSelf == false || HUDCanvas.activeSelf == false)
            {
                OptionsContainer.SetActive(false);
                InfoContainer.SetActive(false);
                HUDCanvas.SetActive(false);
            }
        System.Console.WriteLine("Start app");*/
    }
    public void StartGame(string gameplayScene)
    {
        SceneManager.LoadSceneAsync(gameplayScene);
    }

    public void OptionsOpen()
    {
        OptionsContainer.SetActive(true);
        ButtonContainer.SetActive(false);
    }

    public void OptionsClose()
    {
        OptionsContainer.SetActive(false);
        ButtonContainer.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
