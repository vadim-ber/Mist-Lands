using UnityEngine;

[CreateAssetMenu(fileName = "AISelector", menuName = "ScriptableObjects/create AISelector")]
public class AISelector : Selector
{   
    public override void Initialize(Team team)
    {
        base.Initialize(team);        
        _selectedUnit = null;
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
            Debug.Log(_selectedUnit.ToString());
            InvokeOnUnitSelected(_selectedUnit);
        }
    }
}
