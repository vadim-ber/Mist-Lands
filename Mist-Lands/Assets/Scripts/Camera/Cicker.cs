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
        if (mouse == null)
        {
            return;
        }
        if(_selectedUnit == null)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(mouse.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject target = hit.transform.gameObject;
            if(!target.CompareTag("Unit"))
            {
                Cursor.SetCursor(_cursorData.BaseCursor, Vector2.zero, CursorMode.Auto);
                _targetFinded = false;
                return;
            }
            else
            {
                UnitList.AllUnitsDictonary.TryGetValue(target, out Unit unit);
                if (unit.Team == _selectedUnit.Team)
                {
                    return;
                }
                else
                {
                    if(!_unitsInRadius.Contains(unit))
                    {
                        return;
                    }
                    else
                    {
                        if(_selectedUnit.Combat == Unit.CombatMode.Meele)
                        {
                            Cursor.SetCursor(_cursorData.MeeleAttackCursor, Vector2.zero, CursorMode.Auto);
                        }
                        if (_selectedUnit.Combat == Unit.CombatMode.Ranged)
                        {
                            Cursor.SetCursor(_cursorData.RangedAttackCursor, Vector2.zero, CursorMode.Auto);
                        }
                        _targetFinded = true;
                        HandleAttack(_selectedUnit, unit);
                    }
                }
            }
        }
    }

    protected override void HandleAttack(Unit attacker, Unit defenced)
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log($"{attacker.name} атакует {defenced.name}!");
        }
    }
}
