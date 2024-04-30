using UnityEngine;

[CreateAssetMenu(fileName = "UnitAttackState", menuName = "ScriptableObjects/FSM/Create UnitAttackState")]
public class UnitAttackState : UnitState
{
    private bool _damageApplied;
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Selector.SelectedUnit != unit)
        {            
            SwitchState((UnitState)Transitions.List[0], unit);
        }
        if (unit.Team.State is TeamNotSelected)
        {
            SwitchState((UnitState)Transitions.List[0], unit);
        }
        if (unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f 
            && _damageApplied == true)
        {
            SwitchState((UnitState)Transitions.List[1], unit);
        }
    }

    public override void EnterState(Unit unit)
    {
        _damageApplied = false; 
        if(unit.TargetUnit != null)
        {
            unit.StartCoroutine(unit.WaitRotationTo(unit.TargetUnit.transform.position));
        }        
        unit.Obstacle.enabled = false;
        unit.Agent.enabled = false;
        unit.Selector.AttackInvoked = false;
        unit.CurrentActionPoints -= unit.Weapon.AttackPrice;
        unit.Animator.StopPlayback();
        unit.Animator.Play(CurrentStateAnimationName, AnimationLayer);        
    }

    public override void ExitState(Unit unit)
    {        
        
    }

    public override void UpdateState(Unit unit)
    {
        ApplyDamage(unit);
        CheckSwitchState(unit);
    }    

    private void ApplyDamage(Unit unit)
    {
        if(_damageApplied == true)
        {
            return;
        }
        if(unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f
            && _damageApplied == false)
        {
            _damageApplied = true;
            unit.TargetUnit.Health.TakeDamage(unit.Weapon.Damage);
        }
    }
}
