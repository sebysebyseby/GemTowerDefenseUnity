using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public GameObject[] groundTiles;
    public GameObject[] pathTiles;
    public GameObject checkpointTile;

    public static char[,] bigBoard = new char[19, 20]
    {
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','2','P','P','P','P','P','P','P','P','P','P','3'},
        {'G','G','G','G','G','G','G','G','P','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','P','G','G','G','G','G','G','G','G','G','G','G'},
        {'0','P','P','P','P','P','P','P','1','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G','G'}
    };

    public static char[,] smallBoard = new char[10, 11]
    {
        {'G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G'},
        {'0','P','P','1','G','G','G','G','G','G','G'},
        {'G','G','G','P','G','G','G','G','G','G','G'},
        {'G','G','G','P','G','G','G','4','P','P','5'},
        {'G','G','G','P','G','G','G','P','G','G','G'},
        {'G','G','G','2','P','P','P','3','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G'},
        {'G','G','G','G','G','G','G','G','G','G','G'}
    };

    public char[,] board = bigBoard;

    // Use this for initialization
    void Start () {
        InitializeBoard();
	}

    private void InitializeBoard()
    {
        int height = board.GetLength(0);
        int width = board.GetLength(1);
        for (int i = 0; i<width; i++)
        {
            for (int j = 0; j<height; j++)
            {
                switch (board[j, i])
                    // I think I'll need to set the parent of these things, to make them appear on the canvas layers correctly
                {
                    case 'G':
                        Instantiate(groundTiles[UnityEngine.Random.Range(0,groundTiles.Length)], new Vector3(i, height - j - 1, 0), Quaternion.identity);
                        break;
                    case 'P':
                        Instantiate(pathTiles[0], new Vector3(i, height - j - 1, 0), Quaternion.identity);
                        break;
                    default:
                        Instantiate(pathTiles[1], new Vector3(i, height - j - 1, 0), Quaternion.identity);
                        break;
                }
                    
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
