using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Events;

public class DropZone : MonoBehaviour, IDropHandler
{
    #region Attributes
    [SerializeField] private Vector2 position = Vector2.zero;

    private Draggable _attachedDraggable = null;
    #endregion

    #region Properties
    public int ExpectedIndex = -1;
    public Draggable Draggable;
    public Vector2 Position { get { return position; } }
    public Draggable AttachedDraggrable
    {
        get { return _attachedDraggable; }
    }
    #endregion

    #region Events
    public UnityEvent<Draggable> OnDropped = new UnityEvent<Draggable>();
    #endregion

    #region Methods
    public void OnDrop(PointerEventData eventData)
    {
        // Avoid dropping more than two objects in the same dropzone
        if (transform.childCount >= 2)
        {
            return;
        }

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            Draggable = d;
            d.UpdateDropZone(this);
            d._parentToReturnTo = transform;

            d.OnDropped.Invoke();

            _attachedDraggable = d;
        }
    }
    public void OnDrop(Draggable d)
    {
        Draggable = d;
        d.UpdateDropZone(this);
        d._parentToReturnTo = transform;
        d.OnDropped.Invoke();

        _attachedDraggable = d;
    }

    public void ResetDropZone()
    {
        _attachedDraggable = null;
    }
    #endregion
}