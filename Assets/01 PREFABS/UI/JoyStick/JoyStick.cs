using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform centerTrans;
    [SerializeField] private RectTransform backgroundTrans;
    [SerializeField] private RectTransform thumbStickTrans;
    private bool isDragging;

    public delegate void OnStickInputValueUpdated(Vector2 inputVal);
    public delegate void OnStickTaped();

    public event OnStickInputValueUpdated onStickValueUpdated;
    public event OnStickTaped onStickTaped;


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPos = eventData.position;
        Vector2 centerPos = backgroundTrans.position;

        Vector2 localOffset = Vector2.ClampMagnitude(touchPos - centerPos, backgroundTrans.sizeDelta.x / 2);

        Vector2 inputVal = localOffset / (backgroundTrans.sizeDelta.x / 2);

        thumbStickTrans.position = localOffset + centerPos;

        onStickValueUpdated?.Invoke(inputVal);
        isDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundTrans.position = eventData.position;
        thumbStickTrans.position = eventData.position;
        isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        backgroundTrans.position = centerTrans.position;
        thumbStickTrans.position = backgroundTrans.position;

        onStickValueUpdated?.Invoke(Vector2.zero);

        if (!isDragging)
            onStickTaped?.Invoke();
    }
}
