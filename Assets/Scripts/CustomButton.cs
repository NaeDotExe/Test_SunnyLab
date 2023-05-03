using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(EventTrigger))]
public class CustomButton : Button
{
    #region Attributes
    [SerializeField] private float _defaultScale = 1f;
    [SerializeField] private float _onHoverScale = 1.1f;
    [SerializeField] private float _onClickedScale = 1.2f;
    #endregion 

    #region Methods
    protected override void Start()
    {
        base.Start();
    }

    public void OnSelected()
    {
        transform.DOScale(_onHoverScale, 0.3f);
    }
    public void OnUnselected()
    {
        transform.DOScale(_defaultScale, 0.3f);
    }
    public void OnClicked()
    {
        transform.DOPunchScale(Vector3.one * _onClickedScale, 0.5f);
    }
    #endregion
}
