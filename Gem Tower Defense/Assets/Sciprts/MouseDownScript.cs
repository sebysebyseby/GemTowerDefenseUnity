using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownScript : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
