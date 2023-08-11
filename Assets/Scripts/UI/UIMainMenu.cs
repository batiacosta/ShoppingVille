using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    private void Start()
    {
        playButton.onClick.AddListener(()=>
        {
            SceneManager.LoadScene("Scenes/Scene_Map_01");
        });
        quitButton.onClick.AddListener(Application.Quit);
    }
}
