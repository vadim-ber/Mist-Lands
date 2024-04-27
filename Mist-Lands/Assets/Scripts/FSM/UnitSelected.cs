using UnityEngine;

[CreateAssetMenu(fileName = "Selected", menuName = "ScriptableObjects/FSM/Create Selected state")]
public class UnitSelected : UnitState
{   
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Selector.HasNewSelectedPosition)
        {
            unit.Selector.HasNewSelectedPosition = false;
            SwitchState(Transitions[1], unit);
        }        
        if (unit.Team.Mode is Team.TeamMode.AIControlled && unit.PathIsCompleted)
        {           
            if (unit.AttacksArePossible && unit.FindedUnits.Count > 0
                && unit.CurrentActionPoints >= unit.Weapon.AttackPrice)
            {
                unit.TargetUnit = unit.FindedUnits[0];
                SwitchState(Transitions[2], unit);
            }
            else
            {                
                unit.AttacksArePossible = false;
            }
            unit.Selector.AttackInvoked = false;
        }
        if (unit.Team.Mode is Team.TeamMode.PlayerControlled
            && unit.Selector.AttackInvoked && unit.CurrentActionPoints >= unit.Weapon.AttackPrice)
        {
            unit.Selector.AttackInvoked = false;
            SwitchState(Transitions[2], unit);
        }
        if (unit.Selector.SelectedUnit != unit)
        {
            SwitchState(Transitions[0], unit);
        }
        if (unit.Team.State is TeamNotSelected)
        {
            SwitchState(Transitions[0], unit);
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
