using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    private EventSystem eventSystem;

    public GameObject[] groundTiles;
    public GameObject[] pathTiles;
    public GameObject rockTile;
    public GameObject[] chippedGems;

    private GameStates gameState;

    public Stack<GameObject> freshlyPlacedTiles = new Stack<GameObject>();
    public List<GameObject> placedGems = new List<GameObject>();

    public GameObject lastSelectedGem;

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
    void Start()
    {
        eventSystem = EventSystem.current;
        InitializeBoard();
        gameState = GameStates.BETWEEN_WAVES;
    }

    private void InitializeBoard()
    {
        int height = board.GetLength(0);
        int width = board.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                switch (board[j, i])
                // I think I'll need to set the parent of these things, to make them appear on the canvas layers correctly
                {
                    case 'G':
                        Instantiate(groundTiles[UnityEngine.Random.Range(0, groundTiles.Length)], new Vector3(i, height - j - 1, 0), Quaternion.identity);
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

    public void PlaceRandomTile(float x, float y)
    {
        if (freshlyPlacedTiles.Count >= 5 ) throw new Exception("There were already 5 freshly placed gems");
        if (gameState != GameStates.PLACING_GEMS) throw new Exception("Right now it is not time for gems to be placed");

        Debug.Log("place random tile got called with" + x + "  - " + y);
        var freshlyPlacedGem = Instantiate(chippedGems[UnityEngine.Random.Range(0, chippedGems.Length)], new Vector3(x, y, 0), Quaternion.identity);
        freshlyPlacedTiles.Push(freshlyPlacedGem);
        lastSelectedGem = freshlyPlacedGem;

        if (freshlyPlacedTiles.Count >= 5)
        {
            GameObject.Find("ButtonKeep").GetComponent<Button>().interactable = true;
        }
    }

    public void PlaceRockTile(float x, float y)
    {
        var newRock = Instantiate(rockTile, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void ChangeGameStateToPlacingGems()
    {
        gameState = GameStates.PLACING_GEMS;
    }

    public void KeepSelectedGem()
    {
        if (freshlyPlacedTiles.Count == 5)
        {
            foreach (GameObject freshGem in freshlyPlacedTiles)
            {
                if (freshGem != lastSelectedGem)
                {
                    PlaceRockTile(freshGem.transform.position.x, freshGem.transform.position.y);
                    Destroy(freshGem);
                }
            }
            Debug.Log("I have decided to keep this gem I selected");
            eventSystem.SetSelectedGameObject(lastSelectedGem);
        }
        else
        {
            Debug.Log("I failed to keep a gem because there weren't 5 selected");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
