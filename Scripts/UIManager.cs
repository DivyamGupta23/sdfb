using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public float initial_x;
    public float initial_y;
    public float final_x;
    public float final_y;
    public float duration;

    private void OnEnable()
    {
        SlideIn();
    }  
    private void OnDisable()
    {
        SlideOut();
    }
    public void SlideIn()
    {
        rectTransform.anchoredPosition = new Vector2(initial_x, initial_y);
        canvasGroup.alpha = 0;

        rectTransform.DOAnchorPos(new Vector2(final_x, final_y), duration);
        canvasGroup.DOFade(1, duration);
    }
    public void SlideOut()
    {
        rectTransform.anchoredPosition = new Vector2(initial_x, initial_y);
        canvasGroup.alpha = 1;

        rectTransform.DOAnchorPos(new Vector2(initial_x, initial_y), duration);
        canvasGroup.DOFade(0, duration);
    }
}
