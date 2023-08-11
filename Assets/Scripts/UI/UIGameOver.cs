using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private List<Color> loseColors;
    [SerializeField] private List<Color> winColors;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI goldResultText;
    [SerializeField] private TextMeshProUGUI uniformsResultText;
    [SerializeField] private TextMeshProUGUI weaponsResultText;
    [SerializeField] private TextMeshProUGUI bootsResultText;
    [SerializeField] private Image backGround;
    [SerializeField] private string loseText;
    [SerializeField] private string winText;

    private List<Color> _selectedColors = new List<Color>();
    private string result;
    private bool _playerLose = false;
    private InventorySO _playerInventorySo;
    private void OnEnable()
    {
        _playerLose = GameManager.Instance.DidLose();
        _playerInventorySo = Player.Instance.GetPlayerInventory();
        if (_playerLose)
        {
            _selectedColors = loseColors;
            result = loseText;
        }
        else
        {
            _selectedColors = winColors;
            result = winText;
        }
        SetText();
        SetBackground();
        SetTextResults();
    }

    private void SetText()
    {
        resultText.text = result;
        resultText.color = _selectedColors[0];
    }

    private void SetTextResults()
    {
        goldResultText.text = GameManager.Instance.GetGold().ToString();
        uniformsResultText.text = _playerInventorySo.GetUniformsSo().Count.ToString();
        weaponsResultText.text = _playerInventorySo.GetWeaponsSo().Count.ToString();
        bootsResultText.text = _playerInventorySo.GetBootsSo().Count.ToString();
    }

    private void SetBackground()
    {
        backGround.color = _selectedColors[1];
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Scene_MainMenu");
    }
}
