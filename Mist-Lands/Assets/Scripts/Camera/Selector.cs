using System;
using UnityEngine;

public abstract class Selector : MonoBehaviour
{
    protected Unit _selectedUnit;
    protected Vector3 _selectedPosition;
    public event Action<Unit> OnNewUnitSelected;
    public event Action<Vector3> OnNewPositionSelected;

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
