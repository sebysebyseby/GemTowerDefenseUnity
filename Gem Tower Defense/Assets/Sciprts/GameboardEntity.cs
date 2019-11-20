using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GameboardEntity : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public string type;
    private EventSystem eventSystem;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public virtual void Start()
    {
        eventSystem = EventSystem.current;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }
}
