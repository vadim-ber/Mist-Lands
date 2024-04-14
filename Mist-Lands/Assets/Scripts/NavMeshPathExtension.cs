using UnityEngine;
using UnityEngine.AI;

public static class NavMeshPathExtension 
{
    public static float CalculatePathLength(this NavMeshPath path)
    {
        float length = 0.0f;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            length += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return length;
    }

    public static Vector3 GetPointAtDistance(this NavMeshPath path, float distance)
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            float segmentLength = Vector3.Distance(path.corners[i], path.corners[i + 1]);

            if (segmentLength >= distance)
            {
                Vector3 direction = (path.corners[i + 1] - path.corners[i]).normalized;
                return path.corners[i] + direction * distance;
            }
            else
            {
                distance -= segmentLength;
            }
        }
        return path.corners[^1];
    }
}
