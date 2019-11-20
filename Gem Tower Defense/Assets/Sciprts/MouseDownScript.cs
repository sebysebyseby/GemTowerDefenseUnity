using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private EventSystem eventSystem;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("I clicked the thing");
    }

    private void DarkenSpriteColor()
    {
        Color newColor = spriteRenderer.color;
        newColor.a = 0.8f;
        spriteRenderer.color = newColor;
    }

    private void RestoreSpriteColor()
    {
        Color newColor = spriteRenderer.color;
        newColor.a = 1f;
        spriteRenderer.color = newColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("I got selected");
        RestoreSpriteColor();
        RemoveDescription();
    }

    private void RemoveDescription()
    {
        throw new NotImplementedException();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("I got deselected");
        DarkenSpriteColor();
        UpdateDescription();
    }

    private void UpdateDescription()
    {
        IDescribable objectWithDescription = gameObject.GetComponent<Enemy>();
        Debug.Log("I', updating the description");
        objectWithDescription.UpdateDescription();
    }
}
