using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRestartPanel : MonoBehaviour
{
    public GameObject BtnRestart;

    void Start()
    {
        GameManager.instance.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        BtnRestart.SetActive(true);
    }

    public void OnBtnRestartClick()
    {
        GameManager.instance.Restart();
        BtnRestart.SetActive(false);
    }
}