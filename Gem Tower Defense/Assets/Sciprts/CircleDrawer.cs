using UnityEngine;

public static class CircleDrawer
{

    public static void DrawCircle(this GameObject container, float radius, float lineWidth)
    {
        Material mat = Resources.Load("SelectedCircle", typeof(Material)) as Material;

        var segments = 360;
        var line = container.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.startColor = Color.blue;
        line.endColor = Color.cyan;
        line.material = mat;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius,  Mathf.Cos(rad) * radius, -2);
        }

        line.SetPositions(points);
    }
}