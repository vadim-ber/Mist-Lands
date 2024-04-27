using System.Linq;
using UnityEngine;
public class AISelector : Selector
{    
    public AISelector(Team team) : base(team)
    {             
        _selectedUnit = null;
    }

    public override void UpdateSelector(Transform transform)
    {        
        if (_team.State is not TeamSelected)
        {
            return;
        }
        if (_team.ActiveUnits.All(unit => unit.AttacksArePossible == false))
        {
            _team.EndCurrentTurn();
            return;
        }
        if (_team.ActiveUnits.Count < 1)
        {
            return;
        }
        if (_selectedUnit == null || _selectedUnit.AttacksArePossible == false)
        {                    
            int nextIndex = (_team.ActiveUnits.IndexOf(_selectedUnit) + 1) % _team.ActiveUnits.Count;
            _selectedUnit = _team.ActiveUnits[nextIndex];
        }
        if (_selectedUnit.FindedUnits != null && _selectedUnit.FindedUnits.Count > 0)
        {   
            _selectedUnit.TargetUnit = _selectedUnit.FindedUnits[0];
            InvokeOnAttackIsPossible(_selectedUnit.FindedUnits.ToArray());
            _attackInvoked = true;
        }        
        Vector3 targetPosition = new AiActions(_selectedUnit).CalcVectorToMove();
        
        _selectedPosition = GetPointOnSphereSurface(targetPosition, _selectedUnit.transform.position,
            _selectedUnit.Weapon.AttackRange);
        InvokeOnPositionSelected(_selectedPosition); 
        _hasNewSelectedPosition = true;
    }
}
