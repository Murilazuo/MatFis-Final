using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Triangle : MonoBehaviour
{
    [SerializeField] Vector3 rotation;
    [SerializeField] Vector3[] basePoints;
    [SerializeField] List<Vector3> points;
    [SerializeField] int[] triangle;
    [SerializeField] Color[] colors;
    [SerializeField] Transform[] verticesTransform;
    Mesh mesh;
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        mesh.SetTriangles(triangle,0);
        points = new(basePoints);
        mesh.SetVertices(points);

        int i = 0;
        if (verticesTransform.Length > 0)
            foreach(Transform t in verticesTransform)
                t.GetComponent<Renderer>().material.color = colors[i++];
    }


    public void SetRotation(Vector3 newRotation)
    {
        rotation = newRotation;

        float yaw = rotation.x;
        float pitch = 0;
        float roll = rotation.z;

        var cosa = Mathf.Cos(yaw);
        var sina = Mathf.Sin(yaw);

        var cosb = Mathf.Cos(pitch);
        var sinb = Mathf.Sin(pitch);

        var cosc = Mathf.Cos(roll);
        var sinc = Mathf.Sin(roll);

        var Axx = cosa * cosb;
        var Axy = cosa * sinb * sinc - sina * cosc;
        var Axz = cosa * sinb * cosc + sina * sinc;

        var Ayx = sina * cosb;
        var Ayy = sina * sinb * sinc + cosa * cosc;
        var Ayz = sina * sinb * cosc - cosa * sinc;

        var Azx = -sinb;
        var Azy = cosb * sinc;
        var Azz = cosb * cosc;

        for (var i = 0; i < points.Count; i++)
        {
            var px = basePoints[i].x;
            var py = basePoints[i].y;
            var pz = basePoints[i].z;

            Vector3 pointRotation = new()
            {
                x = Axx * px + Axy * py + Axz * pz,
                y = Ayx * px + Ayy * py + Ayz * pz,
                z = Azx * px + Azy * py + Azz * pz
            };
            points[i] = pointRotation;
            if(verticesTransform.Length > 0) verticesTransform[i].localPosition = pointRotation;
        }

        mesh.SetVertices(points);
    }
    readonly float threshold = 0.1f;
    public override bool Equals(object other)
    {
        if (other.GetType() == GetType())
        {
            Triangle temp = (Triangle)other;
            List<Vector3> toCheck = new(temp.points);

            for(int i = 0; i < 3; i++)
            {
                Vector3 point = points[i];

                for (int j = 0; j < toCheck.Count; j++)
                {
                    Vector3 pos = toCheck[j];

                    if (Approximately(pos.x, point.x) &&
                        Approximately(pos.y, point.y))
                    {
                        toCheck.Remove(toCheck[j]);
                        toCheck.TrimExcess();
                        break;
                    }
                }
            }
            return toCheck.Count == 0;
        }
        return false;
    }
    bool Approximately(float a, float b) => Mathf.Abs(a - b) <= threshold;
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    private void OnDrawGizmos()
    {
        Color[] color = new Color[3];
        color[0] = Color.blue;
        color[1] = Color.yellow;
        color[2] = Color.green;

        int count = 0;
        foreach(Vector3 point in points)
        {
            Gizmos.color = color[count++];
            Gizmos.DrawSphere(point + transform.position, 0.1f);
        }
    }
}
