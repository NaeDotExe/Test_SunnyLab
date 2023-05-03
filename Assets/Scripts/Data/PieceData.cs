using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "Data/PieceData")]
public class PieceData : ScriptableObject
{
    #region Attributes
    //[SerializeField] private Sprite _sprite = null;
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private int _index = -1;
    #endregion

    #region Properties
    //public Sprite Sprite
    //{
    //    get { return _sprite; }
    //}
    public Color Color
    {
        get { return _color; }
    }
    public int Index
    {
        get { return _index; }
    }
    #endregion
}
