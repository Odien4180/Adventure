using System.Collections.Generic;
using UnityEngine;

public static class General
{
    public static Vector3 RandomPositionInSphere(this Vector3 center, float radius)
    {
        return center + Random.insideUnitSphere * radius;
    }

    public static List<T> OverlapSphere<T>(this Vector3 center, float radius, int targetLayer = -1)
    {
        Collider[] targets = targetLayer == -1 ?
            Physics.OverlapSphere(center, radius) : Physics.OverlapSphere(center, radius, targetLayer);

        var results = new List<T>();

        foreach (var target in targets)
        {
            if (target.TryGetComponent<T>(out var result) == false)
                continue;

            results.Add(result);
        }
        return results;
    }
}
