using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Selector
{    
    protected Unit _selectedUnit;
    protected Team _team;
    protected Vector3 _selectedPosition;
    protected Vector3 _direction;
    protected UnitList _unitList;
    protected bool _hasNewSelectedPosition;
    protected bool _attackIsPossible;
    protected bool _weaponSwapInvoked;
    protected int _walkableMask;
    public event Action<Unit> OnNewUnitSelected;
    public event Action<Vector3> OnNewPositionSelected;
    public event Action<Unit> OnAttackIsPossible;

    public UnitList UnitList
    {
        get => _unitList;
    }

    public Selector(Team team)
    {
        _unitList = UnityEngine.Object.FindFirstObjectByType<UnitList>();
        _team = team;
    }
    public Unit SelectedUnit
    {
        get => _selectedUnit;
        set => _selectedUnit = value;
    }
    public Vector3 SelectedPosition
    {
        get => _selectedPosition;
    }
    public bool HasNewSelectedPosition
    {
        get => _hasNewSelectedPosition;
        set => _hasNewSelectedPosition = value;
    }
    public bool AttackInvoked
    {
        get => _attackIsPossible;
        set => _attackIsPossible = value;
    }
    public bool WeaponSwapInvoked
    {
        get => _weaponSwapInvoked;
        set => _weaponSwapInvoked = value;
    }
    public abstract void UpdateSelector(Transform transform);
    public virtual void StopListening() { }
    protected virtual void SwapWeapon() { }
    protected void InvokeOnUnitSelected(Unit unit)
    {       
        OnNewUnitSelected?.Invoke(unit);
    }
    protected void InvokeOnPositionSelected(Vector3 position)
    {       
        OnNewPositionSelected?.Invoke(position);
    }
    protected void InvokeOnAttackIsPossible(Unit unit)
    {
        OnAttackIsPossible?.Invoke(unit);
    }

    protected Vector3 GetNearestWalkablePosition(Vector3 target)
    {
        _walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        if (NavMesh.SamplePosition(target, out NavMeshHit navMeshHit,
            _selectedUnit.Agent.radius * 2, _walkableMask))
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
        _direction = target - center;
        _direction.Normalize(); 
        return center + _direction * (radius - 1); 
    }    
}
