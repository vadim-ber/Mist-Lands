using UnityEngine;

public class AiActions 
{
    private Unit _unit;
    private const float _searchRadius = 1000f;
    public AiActions(Unit unit)
    {
        _unit = unit;
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
        return _unit.Selector.FindClosestUnitPosition(_unit, _searchRadius, false);
    }
}
