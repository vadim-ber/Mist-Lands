using UnityEngine;
using UnityEngine.InputSystem;

public class Clicker : Selector
{    
    private Camera _camera;
    private CursorData _cursorData;
    private bool _targetFinded;

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
        HandleMouseTarget();
        if(_targetFinded == true)
        {
            return;
        }
        HandleLeftClick();
        HandleRightClick();        
    }

    private void HandleLeftClick()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider == null)
        {
            return;
        }
        var unit = hit.collider.gameObject.GetComponent<Unit>();
        if (unit && unit.Team == _team)
        {
            _selectedUnit = unit;
            InvokeOnUnitSelected(_selectedUnit);
            return;
        }
        if (_selectedUnit == null)
        {
            return;
        }
        _selectedPosition = GetNearestWalkablePosition(hit.point);
        InvokeOnPositionSelected(_selectedPosition);
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
        var mouse = Mouse.current;
        if (mouse == null || _selectedUnit == null)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(mouse.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return;
        }

        GameObject target = hit.transform.gameObject;
        if (!target.CompareTag("Unit"))
        {
            Cursor.SetCursor(_cursorData.BaseCursor, Vector2.zero, CursorMode.Auto);
            _targetFinded = false;
            return;
        }

        UnitList.AllUnitsDictonary.TryGetValue(target, out Unit unit);
        if (unit == null || unit.Team == _selectedUnit.Team 
            || !_selectedUnit.FindedUnits.Contains(unit))
        {
            return;
        }

        if (_selectedUnit.Weapon.Combat == Weapon.CombatMode.Meele)
        {
            Cursor.SetCursor(_cursorData.MeeleAttackCursor, Vector2.zero, CursorMode.Auto);
        }
        else if (_selectedUnit.Weapon.Combat == Weapon.CombatMode.Ranged)
        {
            Cursor.SetCursor(_cursorData.RangedAttackCursor, Vector2.zero, CursorMode.Auto);
        }

        _targetFinded = true;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _selectedUnit.TargetUnit = unit;
            InvokeOnAttackIsPossible(new Unit[] { unit });
        }
    }
}
