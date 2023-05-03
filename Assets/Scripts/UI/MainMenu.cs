using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Attributes
    [Header("UI")]
    [SerializeField] private GameObject _menu = null; 
    [SerializeField] private Button _buttonStart = null;
    [SerializeField] private Button _buttonLevelSelection = null;
    [SerializeField] private Button _buttonQuit = null;

    [Header("Level Selection Panel")]
    [SerializeField] private GameObject _levelSelection = null;
    [SerializeField] private CustomButton _level1Button = null;
    [SerializeField] private CustomButton _level2Button = null;
    [SerializeField] private CustomButton _menuButton = null;

    [Header("References")]
    [SerializeField] private SceneLoader _sceneLoader = null;
    #endregion

    #region Methods
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _buttonStart.onClick.AddListener(() => _sceneLoader.LoadLevel(1));
        _buttonLevelSelection.onClick.AddListener(OpenLevelSelection);
        _buttonQuit.onClick.AddListener(() => Application.Quit());

        // Level Selection
        _level1Button.onClick.AddListener(() => _sceneLoader.LoadLevel(1));
        _level2Button.onClick.AddListener(() => _sceneLoader.LoadLevel(2));
        _menuButton.onClick.AddListener(OpenMenu);

        OpenMenu();
    }

    public void OpenLevelSelection()
    {
        _menu.SetActive(false);
        _levelSelection.SetActive(true);

        _level1Button.Select();
    }
    public void OpenMenu()
    {
        _menu.SetActive(true);
        _levelSelection.SetActive(false);

        _buttonStart.Select();
    }
    #endregion
}
