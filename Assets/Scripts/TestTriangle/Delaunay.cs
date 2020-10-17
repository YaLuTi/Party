using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TriangleNet.Geometry;
using TriangleNet.Topology;

public static class Delaunay
{
    public static ICollection<Triangle> Triangulate(List<Vector3> points)
    {
        var ptsIn2D = points.Select(p => new Vector2(p.x, p.z)).ToList();
        return Triangulate(ptsIn2D);
    }

    public static Vertex ToVertex(this Vector2 vec2)
    {
        return new Vertex(vec2.x, vec2.y);
    }

    public static Vector2 ToVector2(this Vertex v)
    {
        return new Vector2((float)v.X, (float)v.Y);
    }

    /// <summary>
    /// Calculates the area of the triangle using
    /// vector cross products.
    /// 
    /// It seems Triangle.Net does not provide
    /// anything of the sorts.
    /// </summary>
    public static float TriArea(this Triangle t)
    {
        var p1 = t.GetVertex(0).ToVector2();
        var p2 = t.GetVertex(1).ToVector2();
        var p3 = t.GetVertex(2).ToVector2();
        Vector3 V = Vector3.Cross(p1 - p2, p1 - p3);
        return V.magnitude * 0.5f;
    }

    public static List<Vector2> ToVertexList(this Triangle t)
    {
        var ls = new List<Vector2>();
        for (int i = 0; i < 3; ++i)
        {
            ls.Add(t.GetVertex(i).ToVector2());
        }
        return ls;
    }

    public static ICollection<Triangle> Triangulate(IEnumerable<Vector2> points)
    {
        var poly = new Polygon();
        poly.Add(new Contour(points.Select(p => p.ToVertex())));
        var mesh = poly.Triangulate();
        return mesh.Triangles;
    }
}
