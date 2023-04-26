using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Attributes
    [Header("UI")]
    [SerializeField] private Button _buttonStart = null;

    [Header("References")]
    [SerializeField] private SceneLoader _sceneLoader = null;
    #endregion

    private void Start()
    {
        Init();
    }
    private void Update()
    {

    }

    private void Init()
    {
        _buttonStart.onClick.AddListener(() => _sceneLoader.LoadLevel(1));
    }
}
