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
        if (_team.ActiveUnits.Count < 1)
        {
            return;
        }
        if(_selectedUnit == null)
        {
            _selectedUnit = _team.ActiveUnits[0];
            InvokeOnUnitSelected(_selectedUnit);            
        }
        _selectedPosition = _team.ActiveUnits[1].transform.position;
        InvokeOnPositionSelected(_selectedPosition);
        Debug.Log(_selectedPosition);
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
