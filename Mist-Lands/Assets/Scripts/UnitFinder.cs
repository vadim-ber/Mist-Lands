using System.Collections.Generic;
using UnityEngine;

public class UnitFinder
{
    private readonly Unit _unit;
    private readonly Dictionary<GameObject, Unit> _allUnitsDictonary;
    public UnitFinder(Unit unit, Dictionary<GameObject, Unit> allUnitsDictonary)
    {
        _unit = unit;
        _allUnitsDictonary = allUnitsDictonary; 
    }

    public Vector3 FindClosestUnitPosition(float radius, bool isFriendly)
    {
        float minDistance = radius;
        Unit closestUnit = null;
        foreach (KeyValuePair<GameObject, Unit> entry in _allUnitsDictonary)
        {
            if ((entry.Value.Team == _unit.Team) == isFriendly)
            {
                float dist = Vector3.Distance(entry.Value.transform.position,
                    _unit.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closestUnit = entry.Value;
                }
            }
        }
        if (closestUnit != null)
        {
            return closestUnit.transform.position;
        }
        else
        {
            Debug.LogWarning("No unit found!");
            return _unit.transform.position;
        }
    }

    public List<Unit> FindUnitsInRadius(float radius, bool isFriendly)
    {       
        List<Unit> unitsInRadius = new();
        foreach (KeyValuePair<GameObject, Unit> entry in _allUnitsDictonary)
        {
            if ((entry.Value.Team == _unit.Team) == isFriendly)
            {
                float dist = Vector3.Distance(entry.Value.transform.position,
                    _unit.transform.position);
                if (dist <= radius)
                {
                    unitsInRadius.Add(entry.Value);
                }
            }
        }
        return unitsInRadius;
    }
}
