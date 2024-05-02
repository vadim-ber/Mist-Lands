using UnityEngine;

[CreateAssetMenu(fileName = "Selected", menuName = "ScriptableObjects/FSM/Create Selected state")]
public class UnitSelected : UnitState
{   
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Selector.HasNewSelectedPosition)
        {            
            SwitchState((UnitState)Transitions.List[2], unit);
        }
        if (unit.Team.Mode is Team.TeamMode.AIControlled && unit.PathIsCompleted)
        {
            if (unit.AttacksIsPossible && unit.TargetUnit != null
                && unit.CurrentActionPoints >= unit.Weapon.AttackPrice)
            {                
                SwitchState((UnitState)Transitions.List[3], unit);
            }
            else
            {                
                unit.AttacksIsPossible = false;
            }            
        }
        if (unit.Selector.AttackInvoked && unit.CurrentActionPoints >= unit.Weapon.AttackPrice
            && unit.TargetUnit != null && unit.Team.Mode is Team.TeamMode.PlayerControlled)
        {           
            SwitchState((UnitState)Transitions.List[3], unit);
        }
        if (unit.Selector.SelectedUnit != unit)
        {
            SwitchState((UnitState)Transitions.List[0], unit);
        }
        if (unit.Team.State is TeamNotSelected)
        {
            SwitchState((UnitState)Transitions.List[0], unit);
        }        
    }

    public override void EnterState(Unit unit)
    {        
        unit.Obstacle.enabled = false;
        unit.Outline.enabled = true;
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
        unit.Animator.SetFloat("Speed", 0f);
    }

    public override void ExitState(Unit unit)
    {        
        
    }

    public override void UpdateState(Unit unit)
    {
        CheckSwitchState(unit);
    }
}
