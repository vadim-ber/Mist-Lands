using UnityEngine;

[CreateAssetMenu(fileName = "UnitAttackState", menuName = "ScriptableObjects/FSM/Create UnitAttackState")]
public class UnitAttackState : UnitState
{  
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Selector.SelectedUnit != unit)
        {            
            SwitchState(Transitions[2], unit);
        }
        if (unit.Team.State is TeamNotSelected)
        {
            SwitchState(Transitions[2], unit);
        }
        if (unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            SwitchState(Transitions[0], unit);
        }
    }

    public override void EnterState(Unit unit)
    {
        if(unit.TargetUnit != null)
        {
            unit.StartCoroutine(unit.WaitRotationTo(unit.TargetUnit.transform.position));
        }        
        unit.Obstacle.enabled = false;
        unit.Agent.enabled = false;    
        unit.CurrentActionPoints -= unit.Weapon.AttackPrice;
        unit.Animator.StopPlayback();
        unit.Animator.Play(CurrentStateAnimationName, AnimationLayer);
    }

    public override void ExitState(Unit unit)
    {
        Debug.Log($"{unit.name} заканчивает атаку");
    }

    public override void UpdateState(Unit unit)
    {
        CheckSwitchState(unit);
    }
}
