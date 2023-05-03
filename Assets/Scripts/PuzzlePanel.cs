using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PuzzlePanel : MonoBehaviour
{
    #region Attributes
    [SerializeField] private Transform _piecesParent = null;
    [SerializeField] private PuzzlePiece _puzzlePiecePrefab = null;
    [SerializeField] private GridLayoutGroup _grid = null;
    [SerializeField] private DropZone _dropZonePrefab = null;

    [Header("Buttons")]
    [SerializeField] private CustomButton _validateButton = null;
    [SerializeField] private CustomButton _resetButton = null;

    [Header("Success Panel")]
    [SerializeField] private GameObject _successPanel = null;
    [SerializeField] private CustomButton _nextButton = null;
    [SerializeField] private CustomButton _menuButton = null;

    private List<PuzzlePiece> _spawnedPieces = new List<PuzzlePiece>();
    private List<DropZone> _dropZones = new List<DropZone>();
    #endregion

    #region Properties
    public List<PuzzlePiece> SpawnedPieces
    {
        get { return _spawnedPieces; }
    }
    public List<DropZone> DropZones
    {
        get { return _dropZones; }
    }
    #endregion

    #region Events
    [Space]
    public UnityEvent OnValidatePuzzleRequest = new UnityEvent();
    public UnityEvent OnNextLevelRequest = new UnityEvent();
    public UnityEvent OnMenuRequest = new UnityEvent();
    #endregion

    #region Methods
    private void Start()
    {
        BindEvents();

        DisplaySuccessPanel(false);
    }
    private void BindEvents()
    {
        _validateButton.onClick.AddListener(OnValidatePuzzleRequest.Invoke);
        _resetButton.onClick.AddListener(ResetPanel);
        _nextButton.onClick.AddListener(OnNextLevelRequest.Invoke);
        _menuButton.onClick.AddListener(OnMenuRequest.Invoke);
    }

    public void DisplaySuccessPanel(bool show)
    {
        _successPanel.SetActive(show);

        _validateButton.interactable = !show;
        _resetButton.interactable = !show;

        if (show)
        {
            _nextButton.Select();
        }
        else
        {
            _validateButton.Select();
        }
    }

    public void InitPanel(PuzzleData puzzle)
    {
        // init pieces
        for (int i = 0; i < puzzle.Pieces.Count; ++i)
        {
            PuzzlePiece piece = Instantiate(_puzzlePiecePrefab, _piecesParent);
            piece.SetData(puzzle.Pieces[i]);
            piece.Parent = _piecesParent;
            piece.gameObject.SetActive(true);
            _spawnedPieces.Add(piece);
        }

        // init drop zones
        _grid.constraintCount = puzzle.Columns;

        for (int i = 0; i < puzzle.Pieces.Count; ++i)
        {
            DropZone zone = Instantiate(_dropZonePrefab, _grid.transform);
            zone.ExpectedIndex = puzzle.Pieces[i].Index;
            zone.gameObject.SetActive(true);
            _dropZones.Add(zone);
        }
    }
    public void ResetPanel()
    {
        foreach (PuzzlePiece piece in _spawnedPieces)
        {
            piece.ResetDraggable();
            piece.transform.parent = _piecesParent;
        }

        foreach (DropZone zone in _dropZones)
        {
            zone.ResetDropZone();
        }
    }
    #endregion
}
