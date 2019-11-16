using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownScript : MonoBehaviour, IPointerClickHandler
{
    private BoardManager boardManager;
    // Start is called before the first frame update
    void Start()
    {
        boardManager = (BoardManager)GameObject.Find("GameBoard").GetComponent(typeof(BoardManager));
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
        boardManager.PlaceTile(x, y);
    }
}
