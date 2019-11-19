using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownScript : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    private BoardManager boardManager;
    private EventSystem eventSystem;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        boardManager = (BoardManager)GameObject.Find("GameBoard").GetComponent(typeof(BoardManager));
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("I pointerClickHandled the thing");
        var x = gameObject.transform.position.x;
        var y = gameObject.transform.position.y;
        //boardManager.PlaceRandomTile(x, y);

        eventSystem.SetSelectedGameObject(gameObject);
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
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("I got deselected");
        DarkenSpriteColor();
    }
}
