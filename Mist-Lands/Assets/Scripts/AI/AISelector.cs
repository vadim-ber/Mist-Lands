using System.Linq;
using UnityEngine;
public class AISelector : Selector
{    
    public AISelector(Team team) : base(team)
    {             
        _selectedUnit = null;
        _team.OnTurnEnd += ResetSelectedUnit;
    }

    public override void UpdateSelector(Transform transform)
    {        
        if (_team.State is not TeamSelected)
        {
            return;
        }
        if (_team.ActiveUnits.All(unit => unit.HasFinishedActions))
        {
            _team.EndCurrentTurn();
            return;
        }
        if (_team.ActiveUnits.Count < 1)
        {
            return;
        }
        if (_selectedUnit == null || _selectedUnit.HasFinishedActions)
        {            
            int nextIndex = (_team.ActiveUnits.IndexOf(_selectedUnit) + 1) % _team.ActiveUnits.Count;
            _selectedUnit = _team.ActiveUnits[nextIndex];
            InvokeOnUnitSelected(_selectedUnit);
        }
        if (_selectedUnit.FindedUnits != null && _selectedUnit.FindedUnits.Count > 0)
        {   
            _selectedUnit.TargetUnit = _selectedUnit.FindedUnits[0];
            InvokeOnAttackIsPossible(_selectedUnit.FindedUnits.ToArray());
        }        
        Vector3 targetPosition = new AiActions(_selectedUnit).CalcVectorToMove();
        
        _selectedPosition = GetPointOnSphereSurface(targetPosition, _selectedUnit.transform.position,
            _selectedUnit.Weapon.AttackRange);
        InvokeOnPositionSelected(_selectedPosition);        
    }

    public override void StopListening()
    {
        _team.OnTurnEnd -= ResetSelectedUnit;
    }

    private void ResetSelectedUnit()
    {
        _selectedUnit = null;
    }
}
