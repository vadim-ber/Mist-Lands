using UnityEngine;

[CreateAssetMenu(fileName = "AISelected", menuName = "ScriptableObjects/FSM/Create AISelected state")]
public class AISelected : UnitState
{
    public override void CheckSwitchState(Unit unit)
    {
        if(unit.Team.State is not TeamSelected)
        {
            SwitchState(Transitions[0], unit);
        }
        if(!unit.Team.AITeam)
        {
            SwitchState(Transitions[0], unit);
        }
    }

    public override void EnterState(Unit unit)
    {
        unit.Outline.enabled = true;
        if (unit.Animator == null)
        {
            return;
        }
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
    }

    public override void ExitState(Unit unit)
    {
        
    }

    public override void UpdateState(Unit unit)
    {
        CheckSwitchState(unit);
    }
}
