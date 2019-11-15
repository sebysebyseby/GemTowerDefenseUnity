using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownScript : MonoBehaviour, IPointerClickHandler
{
    public BoardManager boardManager;
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
        boardManager.PlaceTile(4, 5);
    }
}
