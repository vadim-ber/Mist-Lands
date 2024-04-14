using System.Collections.Generic;
using UnityEngine;

public class AiActions 
{
    private Unit _unit;
    private List<Unit> _allUnits;
    private const float _maxRange = 10000f;
    public AiActions(Unit unit, List<Unit> allUnits)
    {
        _unit = unit;
        _allUnits = allUnits;
    }
    public Vector3 CalcVectorToMove()
    {
        return _unit.Combat switch
        {
            Unit.CombatMode.Meele => HandleMeeleCombat(),
            Unit.CombatMode.Ranged => HandleRangedCombat(),
            _ => Vector3.zero,
        };
    }

    private Vector3 HandleMeeleCombat()
    {
        return Convergence();
    }

    private Vector3 HandleRangedCombat()
    {
        return Convergence();
    }

    private Vector3 Convergence()
    {
        float minDistance = _maxRange;
        Unit closestUnit = null;    
        foreach(Unit unit in _allUnits)
        {
            if(unit.Team != _unit.Team)
            {
                float dist = Vector3.Distance(unit.transform.position,
                    _unit.transform.position);
                if(dist < minDistance)
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
            return Vector3.zero; 
        }
    }
}
