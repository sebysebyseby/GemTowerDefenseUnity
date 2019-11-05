using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler {

    // Use this for initialization
    void Start()
    {
        Debug.Log("I instantiated a tile");

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("I clicked a tile");
        SayHello();
    } 

    public void SayHello()
    {
        Debug.Log("I definitely clicked a tile");
    }
}