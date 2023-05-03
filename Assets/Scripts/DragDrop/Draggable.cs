using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Attributes
    [HideInInspector] public Transform _parentToReturnTo = null;
    [HideInInspector] public DropZone _dropZone;

    [SerializeField] private Transform _parent;

    private Vector2 _originalPosition;
    private bool _dropZoneUpdated;
    private bool _isDraggable = true;

    [HideInInspector] public Vector2 CasePosition = Vector2.zero;
    #endregion

    #region Properties
    public bool RaycastTarget
    {
        get { return GetComponent<Image>().raycastTarget; }
        set { GetComponent<Image>().raycastTarget = value; }
    }
    public bool IsDraggable
    {
        get { return _isDraggable; }
        set { _isDraggable = value; }
    }
    public Transform Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }
    #endregion

    #region Events
    public UnityEvent OnDragStart = new UnityEvent();
    public UnityEvent OnCancelDrag = new UnityEvent();
    public UnityEvent OnDropped = new UnityEvent();
    #endregion

    #region Methods
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isDraggable)
        {
            return;
        }

        _parentToReturnTo = transform.parent;
        _originalPosition = GetComponent<RectTransform>().anchoredPosition;
        transform.SetParent(_parent);
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        OnDragStart.Invoke();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDraggable)
        {
            return;
        }

        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isDraggable)
        {
            return;
        }

        if (_dropZone == null)
        {
            transform.SetParent(_parentToReturnTo);
            GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchoredPosition = _originalPosition;

            OnCancelDrag.Invoke();

        }
        else if (_dropZoneUpdated)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

            transform.SetParent(_dropZone.transform);
            GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            CasePosition = _dropZone.Position;
        }
        else
        {
            transform.SetParent(_parentToReturnTo);

            GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            OnCancelDrag.Invoke();
        }

        _dropZoneUpdated = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void ForceEndDrag()
    {
        if (!_isDraggable)
        {
            return;
        }

        if (_dropZone == null)
        {
            transform.SetParent(_parentToReturnTo);
            GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchoredPosition = _originalPosition;

            OnCancelDrag.Invoke();

        }
        else if (_dropZoneUpdated)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

            transform.SetParent(_dropZone.transform);
            GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            CasePosition = _dropZone.Position;
        }
        else
        {
            transform.SetParent(_parentToReturnTo);

            GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            OnCancelDrag.Invoke();
        }

        _dropZoneUpdated = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void UpdateDropZone(DropZone d)
    {
        if (!_isDraggable)
        {
            return;
        }

        if (_dropZone != null)
        {
            _dropZone.Draggable = null;
        }

        _dropZone = d;
        _dropZoneUpdated = true;
    }

    public void ResetDraggable()
    {
        transform.SetParent(_parentToReturnTo);
        GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        GetComponent<RectTransform>().anchoredPosition = _originalPosition;

        _dropZoneUpdated = false;
        _isDraggable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    #endregion
}