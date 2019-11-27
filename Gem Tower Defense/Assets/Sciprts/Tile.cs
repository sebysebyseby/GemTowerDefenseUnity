using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    private BoardManager boardManager;

    void Start()
    {
        boardManager = (BoardManager)GameObject.Find("GameBoard").GetComponent(typeof(BoardManager));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("a tile was clicked");
        boardManager.PlaceRandomTile(gameObject.transform.position.x, gameObject.transform.position.y);
    }
}
