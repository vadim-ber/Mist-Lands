using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Selector
{
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

    protected void SelectedPositionNormalized(Vector3 target)
    {
        int walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        if (NavMesh.SamplePosition(target, out NavMeshHit navMeshHit,
            _selectedUnit.Agent.height, walkableMask))
        {
            _selectedPosition = navMeshHit.position;
        }
    }
}