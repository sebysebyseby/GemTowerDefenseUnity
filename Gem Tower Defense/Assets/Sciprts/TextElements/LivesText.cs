﻿using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour
{
    BoardManager boardManager;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        boardManager = (BoardManager)GameObject.Find("GameBoard").GetComponent(typeof(BoardManager));
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Lives: " + boardManager.lives.ToString();
    }
}
