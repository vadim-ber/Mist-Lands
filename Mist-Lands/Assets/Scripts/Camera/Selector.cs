using System;
using UnityEngine;

public abstract class Selector : ScriptableObject
{
    protected Unit _selectedUnit;
    protected Team _team;
    protected Vector3 _selectedPosition;
    public event Action<Unit> OnNewUnitSelected;
    public event Action<Vector3> OnNewPositionSelected;

    public virtual void Initialize(Team team)
    {
        _team = team;
    }
    public abstract void UpdateSelector(Transform transform);
    
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
}
