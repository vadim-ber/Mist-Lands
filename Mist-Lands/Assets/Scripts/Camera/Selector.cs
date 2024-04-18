using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Selector
{
    protected List<Unit> _unitsInRadius;
    protected Unit _selectedUnit;
    protected Team _team;
    protected Vector3 _selectedPosition;
    protected UnitList _unitList;
    public event Action<Unit> OnNewUnitSelected;
    public event Action<Vector3> OnNewPositionSelected;

    public UnitList UnitList
    {
        get => _unitList;
    }

    public Selector(Team team)
    {
        _unitList = UnityEngine.Object.FindFirstObjectByType<UnitList>();
        _team = team;
    }
    public abstract void UpdateSelector(Transform transform);
    public virtual void StopListening() { }
    public Unit SelectedUnit
    {
        get => _selectedUnit;
    }
    public Vector3 SelectedPosition
    {
        get => _selectedPosition;
    }

    protected void InvokeOnUnitSelected(Unit unit)
    {
        OnNewUnitSelected?.Invoke(unit);
    }

    protected void InvokeOnPositionSelected(Vector3 position)
    {       
        OnNewPositionSelected?.Invoke(position);
    }

    protected Vector3 GetNearestWalkablePosition(Vector3 target)
    {
        int walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        if (NavMesh.SamplePosition(target, out NavMeshHit navMeshHit,
            _selectedUnit.Agent.radius * 2, walkableMask))
        {
            return navMeshHit.position;
        }
        else
        {
            return target;
        }
    }

    protected Vector3 GetPointOnSphereSurface(Vector3 center, Vector3 target, float radius)
    {
        Vector3 direction = target - center;
        direction.Normalize(); 
        return center + direction * (radius - 1); 
    }

    public Vector3 FindClosestUnitPosition(Unit targetUnit, float radius, bool isFriendly)
    {
        float minDistance = radius;
        Unit closestUnit = null;
        foreach (Unit unit in _unitList.AllUnitsList)
        {
            if ((unit.Team == targetUnit.Team) == isFriendly)
            {
                float dist = Vector3.Distance(unit.transform.position,
                    targetUnit.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closestUnit = unit;
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
            return targetUnit.transform.position;
        }
    }

    public List<Unit> FindUnitsInRadius(Unit targetUnit, float radius, bool isFriendly)
    {
        List<Unit> unitsInRadius = new();
        foreach (Unit unit in _unitList.AllUnitsList)
        {
            if ((unit.Team == targetUnit.Team) == isFriendly)
            {
                float dist = Vector3.Distance(unit.transform.position,
                    targetUnit.transform.position);
                if (dist <= radius)
                {
                    unitsInRadius.Add(unit);
                }
            }
        }
        _unitsInRadius = unitsInRadius; 
        if (unitsInRadius.Count > 0)
        {
            return unitsInRadius;
        }
        else
        {            
            return new List<Unit>(); 
        }
    }
}
