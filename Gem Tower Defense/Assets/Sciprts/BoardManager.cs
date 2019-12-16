using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public GameObject lastSelectedGameObject;
    private GameObject currentSelectedGameObject_Recent;

    private List<Checkpoint> checkpoints;

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

    public char[,] board = FlipAndInvertBoard(bigBoard);

    // Flips a visually correct board into an array that has correct X,Y coordinates
    // ie. board[X,Y] gives you the object at X,Y instead of the object at Y,-X
    private static char[,] FlipAndInvertBoard(char[,] visuallyAppealingBoard)
    {
        int dimension0Count = visuallyAppealingBoard.GetLength(0);
        int dimension1Count = visuallyAppealingBoard.GetLength(1);
        char[,] properlyCoordinatedBoard = new char[dimension1Count, dimension0Count];
        for (int i = 0; i < dimension1Count; i++)
        {
            for (int j = 0; j < dimension0Count; j++)
            {
                properlyCoordinatedBoard[i, j] = visuallyAppealingBoard[dimension0Count - j - 1, i];
            }
        }
        return properlyCoordinatedBoard;
    }

    // Use this for initialization
    void Start()
    {
        eventSystem = EventSystem.current;
        InitializeBoard();
        gameState = GameStates.BETWEEN_WAVES;
    }

    private void InitializeBoard()
    {
        int width = board.GetLength(0);
        int height = board.GetLength(1);
        checkpoints = new List<Checkpoint>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                switch (board[x, y])
                // I think I'll need to set the parent of these things, to make them appear on the canvas layers correctly
                {
                    case 'G':
                        Instantiate(groundTiles[UnityEngine.Random.Range(0, groundTiles.Length)], new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 'P':
                        Instantiate(pathTiles[0], new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 'X':
                        break;
                    default:
                        int index = (int)char.GetNumericValue(board[x, y]);
                        checkpoints.Add(new Checkpoint(index, x, y));
                        Instantiate(pathTiles[1], new Vector3(x, y, 0), Quaternion.identity);
                        break;
                }
            }
        }
        checkpoints = checkpoints.OrderBy(c => c.ZeroIndexCheckpointNumber).ToList();
        foreach (Checkpoint c in checkpoints)
        {
            c.PathToCheckpoint = CalculatePathToCheckpoint(c);
        }
        Debug.Log("done with path calculation");
    }

    // Update is called once per frame
    void Update()
    {
        GetLastGameObjectSelected();
    }

    private void GetLastGameObjectSelected()
    {
        if (eventSystem.currentSelectedGameObject != currentSelectedGameObject_Recent)
        {
            lastSelectedGameObject = currentSelectedGameObject_Recent;
            currentSelectedGameObject_Recent = eventSystem.currentSelectedGameObject;
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

    private List<Vector3> CalculatePathToCheckpoint(Checkpoint c)
    {
        if (c.ZeroIndexCheckpointNumber == 0) return new List<Vector3>();
        Vector3 start = checkpoints[c.ZeroIndexCheckpointNumber - 1].Position;
        Vector3 destination = c.Position;

        // store explored spaces as Tuple<current, path taken>
        Queue<Tuple<Vector3, List<Vector3>>> exploredPaths = new Queue<Tuple<Vector3, List<Vector3>>>();
        List<Vector3> exploredSpaces = new List<Vector3>();
        exploredSpaces.Add(start);
        exploredPaths.Enqueue(Tuple.Create(start, new List<Vector3>()));

        while (exploredPaths.Count > 0)
        {
            var currentPath = exploredPaths.Peek();
            if (currentPath.Item1 == destination)
            {
                currentPath.Item2.Add(destination);
                return currentPath.Item2;
            }

            List<Vector3> spacesAvailable = FindNextSpace(exploredSpaces, currentPath.Item1);
            foreach(var currentSpace in spacesAvailable)
            {
                var path = new List<Vector3>(currentPath.Item2);
                path.Add(currentPath.Item1);
                exploredSpaces.Add(currentSpace);
                exploredPaths.Enqueue(Tuple.Create(currentSpace, path));
            }
            exploredPaths.Dequeue();
        }
        return null;
    }
    
    private List<Vector3> FindNextSpace(List<Vector3> exploredSpaces, Vector3 currentSpace)
    {
        List<Vector3> emptySpaces = new List<Vector3>();
        int width = board.GetLength(0);
        int height = board.GetLength(1);
        int x = (int)currentSpace.x;
        int y = (int)currentSpace.y;

        // try move up -> left -> right -> down
        if ((y + 1) < height && board[x, y + 1] != 'X') emptySpaces.Add(new Vector3(x, y + 1, 0));
        if ((x - 1) >= 0     && board[x - 1, y] != 'X') emptySpaces.Add(new Vector3(x - 1, y, 0));
        if ((x + 1) < width  && board[x + 1, y] != 'X') emptySpaces.Add(new Vector3(x + 1, y, 0));
        if ((y - 1) >= 0     && board[x, y - 1] != 'X') emptySpaces.Add(new Vector3(x, y - 1, 0));
        return emptySpaces.Except(exploredSpaces).ToList();
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

        if (freshlyPlacedTiles.Count == 5 && freshlyPlacedTiles.Contains(lastSelectedGameObject))
        {
            foreach (GameObject freshGem in freshlyPlacedTiles)
            {
                if (freshGem != lastSelectedGameObject)
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
            Debug.Log("I failed to keep a gem because there weren't 5 selected or I was trying to keep something irrelevant");
        }

    }
}
