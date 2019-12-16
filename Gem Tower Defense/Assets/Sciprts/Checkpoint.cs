using UnityEngine;
using System.Collections.Generic;

public class Checkpoint
{
    public int ZeroIndexCheckpointNumber { get; set; }
    public Vector3 Position { get; set; }
    public List<Vector3> PathToCheckpoint { get; set; } 

    public Checkpoint(int index, float x, float y)
    {
        ZeroIndexCheckpointNumber = index;
        Position = new Vector3(x, y, 0);
    }
}
