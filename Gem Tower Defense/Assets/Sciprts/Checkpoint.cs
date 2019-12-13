public class Checkpoint
{
    public int ZeroIndexCheckpointNumber { get; set; }
    public float X { get; set; }
    public float Y { get; set; }

    public Checkpoint(int index, float x, float y)
    {
        ZeroIndexCheckpointNumber = index;
        X = x;
        Y = y;
    }
}
