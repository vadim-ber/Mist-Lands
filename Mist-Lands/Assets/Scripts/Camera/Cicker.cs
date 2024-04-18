using UnityEngine;
using UnityEngine.InputSystem;

public class Clicker : Selector
{    
    private Camera _camera;

    public Clicker(Team team) : base(team)
    {        
        _camera = Camera.main;       
    }

    public override void UpdateSelector(Transform transform)
    {        
        if (_team.State is not TeamSelected)
        {
            return;
        }
        HandleLeftClick();
        HandleRightClick();
        HandleMoseTarget();
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

    private void HandleMoseTarget()
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
                return;
            }
            else
            {
                var unit = target.GetComponent<Unit>();
                if(unit.Team == _selectedUnit.Team)
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
                        Debug.Log($"{unit.name} - допустимая цель для {_selectedUnit.name}");
                    }
                }
            }
        }
    }
}
