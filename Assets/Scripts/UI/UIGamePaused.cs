using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGamePaused : MonoBehaviour
{
    public void Resume()
    {
        GameManager.Instance.SetPlay();
        gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Scene_MainMenu");
    }
}
