using System.Collections;
using System.Collections.Generic;
using TriangleNet.Topology;
using UnityEngine;
using System.Linq;

public class Area : MonoBehaviour
{
    public List<Vector3> Corners;

    private List<Triangle> _triangles;
    private double _areaSum;

    private Dictionary<Triangle, List<Vector3>> _linesToDraw = new Dictionary<Triangle, List<Vector3>>();

    public void Awake()
    {
        _triangles = new List<Triangle>();
        _triangles.AddRange(Delaunay.Triangulate(Corners));
        _areaSum = 0f;
        _triangles.ForEach(x => _areaSum += x.TriArea());

        foreach (var tri in _triangles)
        {
            var pts = tri.ToVertexList().Select(p => new Vector3(p.x, 0f, p.y)).ToList();
            pts.Add(pts[0]);
            _linesToDraw[tri] = pts;
        }
    }

    private List<Vector3> SamplePositions(Area a, int count)
    {
        var l = new List<Vector3>();
        for (int i = 0; i < count; ++i)
        {
            l.Add(a.PickRandomLocation());
        }
        return l;
    }

    public Vector3 PickRandomLocation()
    {
        var tri = PickRandomTriangle();
        var randPos = RandomWithinTriangle(tri);
        return new Vector3(randPos.x, 0f, randPos.y);
    }

    private Triangle PickRandomTriangle()
    {
        var rng = Random.Range(0f, (float)_areaSum);
        for (int i = 0; i < _triangles.Count; ++i)
        {
            if (rng < _triangles[i].TriArea())
            {
                return _triangles[i];
            }
            rng -= _triangles[i].TriArea();
        }
        throw new System.Exception("Should not get here.");
    }

    private Vector2 RandomWithinTriangle(Triangle t)
    {
        var r1 = Mathf.Sqrt(Random.Range(0f, 1f));
        var r2 = Random.Range(0f, 1f);
        var m1 = 1 - r1;
        var m2 = r1 * (1 - r2);
        var m3 = r2 * r1;

        var p1 = t.GetVertex(0).ToVector2();
        var p2 = t.GetVertex(1).ToVector2();
        var p3 = t.GetVertex(2).ToVector2();
        return (m1 * p1) + (m2 * p2) + (m3 * p3);
    }

    public void OnDrawGizmos()
    {
        foreach (var k in _linesToDraw.Keys)
        {
            for (int i = 0; i < 3; ++i)
            {
                Gizmos.DrawLine(_linesToDraw[k][i], _linesToDraw[k][i + 1]);
            }
        }

        if (_triangles == null)
        {
            return;
        }
        
    }
}
