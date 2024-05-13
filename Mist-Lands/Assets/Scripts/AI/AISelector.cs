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
        CheckTurnEnd();
        GetNextUnit();
        SelectNewPosition();
        SelectTargetUnit();
    }

    private void CheckTurnEnd()
    {
        if (_team.ActiveUnits.All(unit => unit.AttacksIsPossible == false))
        {
            _team.EndCurrentTurn();
            return;
        }
    }
    private void GetNextUnit()
    {
        if (_team.ActiveUnits.Count < 1)
        {
            return;
        }
        if (_selectedUnit == null || _selectedUnit.AttacksIsPossible == false)
        {
            int nextIndex = (_team.ActiveUnits.IndexOf(_selectedUnit) + 1) % _team.ActiveUnits.Count;
            _selectedUnit = _team.ActiveUnits[nextIndex];
        }
    }

    private void SelectNewPosition()
    {
        if(_selectedUnit == null)
        {
            return;
        }
        Vector3 targetPosition = new AiActions(_selectedUnit).CalcVectorToMove();
        _selectedPosition = GetPointOnSphereSurface(targetPosition, _selectedUnit.transform.position,
            _selectedUnit.CurrentAttackRange);
        InvokeOnPositionSelected(_selectedPosition);
        _hasNewSelectedPosition = true;
    }

    private void SelectTargetUnit()
    {  
        if(_selectedUnit == null)
        {
            return;
        }
        if (_selectedUnit.LastAttacker != null && _selectedUnit.LastAttacker.State is not UnitFall
            && _selectedUnit.FindedUnits.Contains(_selectedUnit.LastAttacker))
        {
            _selectedUnit.TargetUnit = _selectedUnit.LastAttacker;
            _attackInvoked = true;
            return;
        }
        if (_selectedUnit.FindedUnits.Count > 0)
        {
            _selectedUnit.TargetUnit = _selectedUnit.FindedUnits.FirstOrDefault
                (u => u.Team.ActiveUnits.Contains(u));
            _attackInvoked = true;
        }
    }
}
