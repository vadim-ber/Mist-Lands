using UnityEngine;

[CreateAssetMenu(fileName = "NotSelected", menuName = "ScriptableObjects/FSM/Create NOT Selected state")]
public class UnitNotSelected : UnitState
{  
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Health.IsFall)
        {           
            SwitchState((UnitState)Transitions.List[4], unit);
        }
        if (unit.Health.ReceivedDamage && !unit.Health.IsFall)
        {
            SwitchState((UnitState)Transitions.List[5], unit);
        }
        if (unit.Selector.SelectedUnit == unit && unit.Team.State is not TeamNotSelected)
        {           
            SwitchState((UnitState)Transitions.List[1], unit);
        }        
    }        

    public override void EnterState(Unit unit)
    {
        unit.Outline.enabled = false;
        unit.Agent.enabled = false;
        unit.Obstacle.enabled = true;
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
        unit.Animator.SetFloat("Speed", 0f);
    }

    public override void ExitState(Unit unit)
    {        
        unit.Obstacle.enabled = false;
    }  

    public override void UpdateState(Unit unit)
    {
        CheckSwitchState(unit);
    }
}
