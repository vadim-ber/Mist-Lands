using UnityEngine;

[CreateAssetMenu(fileName = "UnitFallState", menuName = "ScriptableObjects/FSM/Create UnitFallState")]
public class UnitFall : UnitState
{
    public override void CheckSwitchState(Unit unit)
    {
       
    }

    public override void EnterState(Unit unit)
    {        
        unit.Obstacle.enabled = true;
        unit.Agent.enabled = false;
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
        unit.Team.ActiveUnits.Remove(unit);
    }

    public override void ExitState(Unit unit)
    {
        unit.Team.AllUnits.Add(unit);
    }

    public override void UpdateState(Unit unit)
    {
        unit.AttacksIsPossible = false;
        CheckSwitchState(unit);
    }
}
