using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "Data/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    #region Attributes
    [SerializeField] private int _columns = 2;
    [SerializeField] private List<PieceData> _pieces = new List<PieceData>();
    #endregion

    #region Properties
    public int Columns
    {
        get { return _columns; }
    }
    public List<PieceData> Pieces
    {
        get { return _pieces; }
    }
    #endregion
}
