using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DebugClickAndSelect : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    private EventSystem eventSystem;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("I got deselected");
        DarkenSpriteColor();
    }
    private void DarkenSpriteColor()
    {
        Color newColor = spriteRenderer.color;
        newColor.a = 0.8f;
        spriteRenderer.color = newColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("I got selected");
        RestoreSpriteColor();
    }
    private void RestoreSpriteColor()
    {
        Color newColor = spriteRenderer.color;
        newColor.a = 1f;
        spriteRenderer.color = newColor;
    }
}
