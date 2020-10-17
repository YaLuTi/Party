using UnityEditor;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Area))]
public class AreaInspector : Editor
{
    private void OnSceneGUI()
    {
        var area = target as Area;
        if (area.Corners.Count <= 2)
        {
            return;
        }

        Handles.color = Color.white;

        var triangles = Delaunay.Triangulate(area.Corners);
        foreach (var tri in triangles)
        {
            var pts = tri.ToVertexList().Select(p => new Vector3(p.x, 0f, p.y)).ToList();
            pts.Add(pts[0]);
            for (int i = 0; i < 3; ++i)
            {
                Handles.DrawLine(pts[i], pts[i + 1]);
            }
        }

        Handles.color = Color.blue;
        for (int i = 0; i < area.Corners.Count; ++i)
        {
            ShowCorner(area, i);
        }
    }

    private Vector3 ShowCorner(Area target, int index)
    {
        Vector3 corner = target.transform.TransformPoint(target.Corners[index]);
        EditorGUI.BeginChangeCheck();
        corner = Handles.DoPositionHandle(corner, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Move Corner");
            EditorUtility.SetDirty(target);
            target.Corners[index] = target.transform.InverseTransformPoint(corner);
        }
        return corner;
    }
}
