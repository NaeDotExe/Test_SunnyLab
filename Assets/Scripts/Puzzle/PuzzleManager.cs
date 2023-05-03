using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    #region Attributes
    [SerializeField] private int _nextLevel = 0;
    [SerializeField] private PuzzleData _currentPuzzle = null;
    [SerializeField] private PuzzlePanel _puzzlePanel = null;
    [SerializeField] private DragDropController _dragDrogController = null;

    [Space]
    [SerializeField] private SceneLoader _sceneLoader = null;
    #endregion

    #region Events
    public UnityEvent OnPuzzleSuccess = new UnityEvent();
    public UnityEvent OnPuzzleFail = new UnityEvent();
    #endregion

    #region Methods
    private void Start()
    {
        BindEvents();

        _puzzlePanel.InitPanel(_currentPuzzle);

        _dragDrogController.BindEvents(_puzzlePanel.SpawnedPieces);
    }
    private void BindEvents()
    {
        _puzzlePanel.OnValidatePuzzleRequest.AddListener(CheckPuzzle);
        _puzzlePanel.OnMenuRequest.AddListener(OpenMenu);
        _puzzlePanel.OnNextLevelRequest.AddListener(OpenNextLevel);
    }
    private void CheckPuzzle()
    {
        for (int i = 0; i < _puzzlePanel.DropZones.Count; ++i)
        {
            PuzzlePiece piece = _puzzlePanel.DropZones[i].AttachedDraggrable.GetComponent<PuzzlePiece>();

            if (_puzzlePanel.DropZones[i].ExpectedIndex != piece.Index)
            {
                // game over
                return;
            }
        }

        _puzzlePanel.DisplaySuccessPanel(true);
    }
    private void OpenMenu()
    {
        _sceneLoader.LoadScene("MainMenu");
    }
    private void OpenNextLevel()
    {
        _sceneLoader.LoadLevel(_nextLevel);
    }
    #endregion
}
