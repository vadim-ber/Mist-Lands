using UnityEngine;
using UnityEngine.InputSystem;

public class Clicker : Selector
{
    private readonly Camera _camera;
    private readonly CursorData _cursorData;
    private bool _isTargetFound;
    private GameObject _target;
    private Vector2 _mousePosition;
    private Ray _cameraRay;

    public Clicker(Team team) : base(team)
    {
        _camera = Camera.main;
        _cursorData = Resources.Load<CursorData>("ScriptableObjects/CursorData");
        Cursor.SetCursor(_cursorData.BaseCursor, Vector2.zero, CursorMode.Auto);
    }

    public override void UpdateSelector(Transform transform)
    {
        if (_team.State is not TeamSelected)
        {
            return;
        }

        UpdateMousePosition();
        HandleMouseTarget();

        if (_isTargetFound)
        {
            return;
        }

        HandleLeftClick();
        HandleRightClick();
        SwapWeapon();
    }

    private void UpdateMousePosition()
    {
        _mousePosition = Mouse.current.position.ReadValue();
        _cameraRay = _camera.ScreenPointToRay(_mousePosition);
    }

    protected override void SwapWeapon()
    {
        if (_selectedUnit == null || _selectedUnit.Team.Mode is not Team.TeamMode.PlayerControlled)
        {
            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _weaponSwapInvoked = true;
            Debug.Log("Weapon Swap");
        }
    }

    private void HandleLeftClick()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }

        if (!Physics.Raycast(_cameraRay, out RaycastHit hit) || hit.collider == null)
        {
            return;
        }

        if (UnitList.AllUnitsDictonary.TryGetValue(hit.collider.gameObject, out Unit unit) && unit.Team == _team)
        {
            _selectedUnit = unit;
            InvokeOnUnitSelected(_selectedUnit);
            return;
        }

        if (_selectedUnit == null || _selectedUnit.State is not UnitSelected
            || _selectedUnit.Team.Mode is not Team.TeamMode.PlayerControlled)
        {
            return;
        }

        _selectedPosition = GetNearestWalkablePosition(hit.point);
        InvokeOnPositionSelected(_selectedPosition);
        _hasNewSelectedPosition = true;
    }

    private void HandleRightClick()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame)
        {
            return;
        }

        _selectedUnit = null;
        InvokeOnUnitSelected(_selectedUnit);
    }

    private void HandleMouseTarget()
    {
        if (Mouse.current == null || _selectedUnit == null)
        {
            return;
        }

        if (!Physics.Raycast(_cameraRay, out RaycastHit hit))
        {
            return;
        }

        _target = hit.transform.gameObject;
        if (!_target.CompareTag("Unit"))
        {
            Cursor.SetCursor(_cursorData.BaseCursor, Vector2.zero, CursorMode.Auto);
            _isTargetFound = false;
            return;
        }

        if (!UnitList.AllUnitsDictonary.TryGetValue(_target, out Unit unit) || unit == null || unit.Team == _selectedUnit.Team
            || !_selectedUnit.FindedUnits.Contains(unit) || !unit.Team.ActiveUnits.Contains(unit))
        {
            return;
        }

        SetCursorBasedOnCombatMode();

        _isTargetFound = true;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _selectedUnit.TargetUnit = unit;
            InvokeOnAttackIsPossible(unit);
            _attackIsPossible = true;
        }
    }

    private void SetCursorBasedOnCombatMode()
    {
        if (_selectedUnit.WeaponInHands.WeaponData.Combat == WeaponData.CombatMode.Meele)
        {
            Cursor.SetCursor(_cursorData.MeeleAttackCursor, Vector2.zero, CursorMode.Auto);
        }
        else if (_selectedUnit.WeaponInHands.WeaponData.Combat == WeaponData.CombatMode.Ranged)
        {
            Cursor.SetCursor(_cursorData.RangedAttackCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
