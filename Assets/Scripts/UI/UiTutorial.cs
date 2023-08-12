using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiTutorial : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private List<GameObject> lessons;

    private int _iteration = 0;
    private void Start()
    {
        actionButton.onClick.AddListener(Continue);
        HideLessons();
        lessons[0].gameObject.SetActive(true);
    }

    private void Continue()
    {
        _iteration++;
        if (_iteration < lessons.Count)
        {
            HideLessons();
            lessons[_iteration].gameObject.SetActive(true);
        }

        if (_iteration == lessons.Count - 1)
        {
            GameManager.Instance.SetPlay();
            HideLessons();
            _iteration = 0;
            gameObject.SetActive(false);
        }
    }

    private void HideLessons()
    {
        foreach (var lesson in lessons)
        {
            lesson.gameObject.SetActive(false);
        }
    }
}
