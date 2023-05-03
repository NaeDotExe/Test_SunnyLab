using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image), typeof(CanvasGroup))]
public class PuzzlePiece : Draggable
{
    #region Attributes
    [SerializeField] private Image _image = null;
    [SerializeField] private CustomButton _button = null;
    [SerializeField] private TextMeshProUGUI _text = null;

    private int _index = -1;
    private PieceData _data = null;
    #endregion

    #region Properties
    public int Index
    {
        get { return _index; }
    }
    public bool IsSelected = false;
    #endregion

    #region Methods
    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            IsSelected = !IsSelected;
        });
    }

    public void SetData(PieceData data)
    {
        _data = data;

        _image.color = _data.Color;
        _index = data.Index;
        _text.text = data.Index.ToString();
    }
    public bool Overlaps(RectTransform tr)
    {
        Rect thisRect = GetWorldSapceRect(GetComponent<RectTransform>());
        Rect otherRect = GetWorldSapceRect(tr);

        return thisRect.Overlaps(otherRect);
    }

    Rect GetWorldSapceRect(RectTransform rt)
    {
        var r = rt.rect;
        r.center = rt.TransformPoint(r.center);
        r.size = rt.TransformVector(r.size);

        return r;
    }
    #endregion
}
