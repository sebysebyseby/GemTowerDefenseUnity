using UnityEngine;

public class DamageLine : MonoBehaviour
{
    private readonly float timeUntilLineFades = 2.5f;
    private readonly float lineWidth = 0.15f;
    private Color lineColor;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private LineRenderer line;
    
    public static void CreateDamageLine(Color lineColor, Vector3 start, Vector3 end)
    {
        GameObject damageLineObject = new GameObject("DamageLine");
        DamageLine damageLine = damageLineObject.AddComponent<DamageLine>();
        damageLine.lineColor = lineColor;
        damageLine.startPosition = start;
        damageLine.endPosition = end;
    }

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.startColor = lineColor;
        line.endColor = lineColor;
        line.material = Resources.Load("SelectedCircle", typeof(Material)) as Material;

        var points = new Vector3[2];
        points[0] = startPosition;
        points[1] = endPosition;
        line.SetPositions(points);

    }

    // Update is called once per frame
    void Update()
    {
        if (lineColor.a <= 0)
        {
            Destroy(gameObject);
            return;
        }
        
        lineColor.a -= Time.deltaTime / timeUntilLineFades;
        line.startColor = lineColor;
        line.endColor = lineColor;
    }
}
