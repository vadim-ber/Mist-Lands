using UnityEngine;

[CreateAssetMenu(fileName = "NotSelected", menuName = "ScriptableObjects/FSM/Create NOT Selected state")]
public class UnitNotSelected :UnitState, IUnitHandler
{
    public bool HasNewUnit { get; set; }

    public override void CheckSwitchState(Unit unit)
    {        
        if(unit.Team.State is not TeamSelected)
        {
            return;
        }
        if(HasNewUnit && unit.Selector.SelectedUnit == unit)
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
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
        unit.Animator.SetFloat("Speed", 0f);
    }

    public override void ExitState(Unit unit)
    {
        unit.Selector.OnNewUnitSelected -= HandleNewUnit;
        unit.Obstacle.enabled = false;
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
