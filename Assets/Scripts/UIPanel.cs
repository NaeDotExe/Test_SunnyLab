using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    #region Attributes
    [SerializeField] protected bool _hideOnAwake = true;
    [SerializeField] protected float _fadeDuration = 0.4f;

    protected CanvasGroup _canvasGroup = null;
    #endregion

    #region Properties
    public bool IsHidden
    {
        get { return _canvasGroup.alpha == 0 ? true : false; }
    }
    public float FadeDuration
    {
        get { return _fadeDuration; }
    }
    #endregion

    #region Methods
    protected void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = (_hideOnAwake == true) ? 0f : 1f;
    }
    protected void Update()
    {
        bool state = !(_canvasGroup.alpha == 0);
        _canvasGroup.blocksRaycasts = state;
        _canvasGroup.interactable = state;
    }

    public void Show()
    {
        Show(false);
    }
    public void Hide()
    {
        Hide(false);
    }
    public void Show(bool realTime = false)
    {
        if (gameObject.name == "PauseMenu")
        {
            print(realTime);
        }
        if (_canvasGroup.alpha.Equals(1))
        {
            return;
        }

        StopAllCoroutines();

        if (_canvasGroup.gameObject.activeInHierarchy)
        {
            StartCoroutine(DisplayCanvas(true, realTime));
        }
        else
        {
            _canvasGroup.alpha = 1.0f;
        }
    }
    public void Hide(bool realTime = false)
    {

        if (_canvasGroup.alpha.Equals(0))
        {
            return;
        }

        StopAllCoroutines();

        if (_canvasGroup.gameObject.activeInHierarchy)
        {
            StartCoroutine(DisplayCanvas(false, realTime));
        }
        else
        {
            _canvasGroup.alpha = 0.0f;
        }
    }

    public void SetCanvasOpacity(float value)
    {
        _canvasGroup.alpha = value;
    }

    IEnumerator DisplayCanvas(bool visibility, bool realTime = false)
    {
        float startValue = _canvasGroup.alpha;
        float counter = 0f;
        float endValue = (visibility == true) ? 1 : 0f;

        while (counter < _fadeDuration)
        {
            //print(Time.unscaledDeltaTime + " " + realTime);
            counter += realTime ? Time.unscaledDeltaTime : Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(startValue, endValue, counter / _fadeDuration);
            yield return null;
        }

        _canvasGroup.alpha = endValue;
    }
    #endregion
}
