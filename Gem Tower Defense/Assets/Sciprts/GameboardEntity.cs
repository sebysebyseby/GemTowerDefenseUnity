using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class GameboardEntity : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler, IDescribable
{
    protected EventSystem eventSystem;
    private SpriteRenderer spriteRenderer;
    public Text description;

    // Start is called before the first frame update
    public virtual void Start()
    {
        eventSystem = EventSystem.current;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        description = GameObject.Find("DescriptionOfSelection").GetComponent<Text>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var x = gameObject.transform.position.x;
        var y = gameObject.transform.position.y;
        Debug.Log("I pointerClicked the object at x=" + x + ", y=" + y);

        eventSystem.SetSelectedGameObject(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("I got Selected");
        DarkenSpriteColor();
        UpdateDescription();
    }

    private void DarkenSpriteColor()
    {
        Color newColor = spriteRenderer.color;
        newColor.a = 0.65f;
        spriteRenderer.color = newColor;
    }

    public abstract void UpdateDescription();

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("I got Delected");
        RestoreSpriteColor();
        RemoveDescription();
    }

    private void RestoreSpriteColor()
    {
        Color newColor = spriteRenderer.color;
        newColor.a = 1f;
        spriteRenderer.color = newColor;
    }

    public void RemoveDescription()
    {
        description.text = null;
    }
}
