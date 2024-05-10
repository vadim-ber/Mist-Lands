
using UnityEngine;

[CreateAssetMenu(fileName = "UnitGetHitState", menuName = "ScriptableObjects/FSM/Create UnitGetHitState")]
public class UnitGetHitState : UnitState
{
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {            
            if(unit == unit.Selector.SelectedUnit)
            {
                SwitchState((UnitState)Transitions.List[1], unit);
            }
            else
            {
                SwitchState((UnitState)Transitions.List[0], unit);
            }
        }
    }

    public override void EnterState(Unit unit)
    {
        unit.Animator.StopPlayback();
        unit.Animator.Play(CurrentStateAnimationName);
        unit.Health.ReceivedDamage = false;
    }

    public override void ExitState(Unit unit)
    {
        unit.Animator.StopPlayback();
    }

    public override void UpdateState(Unit unit)
    {
       CheckSwitchState(unit);  
    }
}
