using UnityEngine;

[CreateAssetMenu(fileName = "NotSelected", menuName = "ScriptableObjects/FSM/Create NOT Selected state")]
public class NotSelected :UnitState, IUnitHandler
{
    public bool HasNewUnit { get; set; }

    public override void CheckSwitchState(Unit unit)
    {
        if(unit.Team.State is not TeamSelected)
        {
            return;
        }
        if(unit.Team.Selector is AISelector)
        {
            SwitchState(Transitions[1], unit);
        }
        if(unit.Team.Selector is not AISelector && HasNewUnit && unit.Selector.SelectedUnit == unit)
        {
            HasNewUnit = false;
            SwitchState(Transitions[0], unit);
        }
    }        

    public override void EnterState(Unit unit)
    {
        unit.Outline.enabled = false;
        unit.Agent.enabled = false;
        unit.Selector.OnNewUnitSelected += HandleNewUnit;
        unit.Obstacle.enabled = true;
        if (unit.Animator == null)
        {
            return;
        }
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
    }

    public override void ExitState(Unit unit)
    {
        unit.Selector.OnNewUnitSelected -= HandleNewUnit;
        unit.Obstacle.enabled = false;
        if (unit.Animator == null)
        {
            return;
        }       
    }  

    public override void UpdateState(Unit unit)
    {
        CheckSwitchState(unit);
    }

    public void HandleNewUnit(Unit unit)
    {
        HasNewUnit = true;
    }
}
