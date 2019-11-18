using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownScript : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    private BoardManager boardManager;
    private EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        boardManager = (BoardManager)GameObject.Find("GameBoard").GetComponent(typeof(BoardManager));
        eventSystem = EventSystem.current;
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
        boardManager.PlaceRandomTile(x, y);

        eventSystem.SetSelectedGameObject(gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("I got selected");
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("I got deselected");
    }
}
