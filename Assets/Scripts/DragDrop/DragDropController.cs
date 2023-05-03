using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropController : MonoBehaviour
{
    #region Attributes
    [SerializeField] private float _moveSpeed = 200f;
    [SerializeField] private PuzzlePanel _puzzlePanel = null;
    #endregion

    #region Properties
    [HideInInspector]
    public PuzzlePiece SelectedPiece = null;
    #endregion

    #region Methods
    public void BindEvents(List<PuzzlePiece> pieces)
    {
        foreach (PuzzlePiece piece in pieces)
        {
            piece.GetComponent<CustomButton>().onClick.AddListener(() =>
            {
                if (SelectedPiece == null)
                {
                    EnableNavigation(false);
                    SelectedPiece = piece;
                    SelectedPiece.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            });
        }
    }

    private void EnableNavigation(bool enable)
    {
        if (enable)
        {
            Selectable[] selectables = FindObjectsOfType<Selectable>();
            foreach (Selectable selectable in selectables)
            {
                Navigation navigation = selectable.navigation;
                navigation.mode = Navigation.Mode.Automatic;
                selectable.navigation = navigation;
            }
        }
        else
        {
            Selectable[] selectables = FindObjectsOfType<Selectable>();
            foreach (Selectable selectable in selectables)
            {
                Navigation navigation = selectable.navigation;
                navigation.mode = Navigation.Mode.None;
                selectable.navigation = navigation;
            }
        }
    }

    private void Update()
    {
        if (SelectedPiece != null /*&& SelectedPiece.IsSelected*/)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, vertical, 0);
            movement = movement * _moveSpeed * Time.deltaTime;

            SelectedPiece.transform.Translate(movement);

            if (Input.GetButtonDown("Fire2"))
            {
                // unselect
                Debug.Log("click");

                bool overlaps = false;
                foreach (DropZone zone in _puzzlePanel.DropZones)
                {
                    // if piece is on top of a drop zone
                    if (SelectedPiece.Overlaps(zone.GetComponent<RectTransform>()))
                    {
                        Debug.Log("overlaps" + zone.gameObject);
                        overlaps = true;

                        zone.OnDrop(SelectedPiece);
                    }
                }

                SelectedPiece.ForceEndDrag();
                SelectedPiece.GetComponent<CanvasGroup>().blocksRaycasts = true;
                SelectedPiece = null;
                EnableNavigation(true);

                GameObject.Find("Button_Validate").GetComponent<Button>().Select();
            }
        }
    }
    #endregion
}
