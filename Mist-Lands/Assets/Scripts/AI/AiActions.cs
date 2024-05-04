using UnityEngine;

public class AiActions 
{
    private readonly Unit _unit;
    private const float _searchRadius = 1000f;
    public AiActions(Unit unit)
    {
        _unit = unit;
    }
    public Vector3 CalcVectorToMove()
    {
        return _unit.Weapon.Combat switch
        {
            WeaponData.CombatMode.Meele => HandleMeeleCombat(),
            WeaponData.CombatMode.Ranged => HandleRangedCombat(),
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
        var finder = new UnitFinder(_unit, _unit.Selector.UnitList.AllUnitsDictonary);
        return finder.FindClosestUnitPosition(_searchRadius, false, true);
    }
}
