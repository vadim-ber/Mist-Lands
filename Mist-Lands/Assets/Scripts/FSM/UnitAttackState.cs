using UnityEngine;

[CreateAssetMenu(fileName = "UnitAttackState", menuName = "ScriptableObjects/FSM/Create UnitAttackState")]
public class UnitAttackState : UnitState
{
    private bool _damageApplied;
    private bool _rotationComplete;
    private Vector3 _rotationTarget;
    public override void CheckSwitchState(Unit unit)
    {
        if(!_rotationComplete)
        {
            return;
        }
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
        _rotationComplete = false;
        _rotationTarget = unit.TargetUnit.transform.position;
        unit.Obstacle.enabled = false;
        unit.Agent.enabled = false;
        unit.Selector.AttackInvoked = false;
        unit.CurrentActionPoints -= unit.WeaponInHands.WeaponData.AttackPrice;
        unit.Animator.StopPlayback();
        unit.Animator.Play(CurrentStateAnimationName, AnimationLayer);        
    }

    public override void ExitState(Unit unit)
    {
        _rotationTarget = Vector3.zero;
        unit.Animator.StopPlayback();
    }

    public override void UpdateState(Unit unit)
    {
        RotateTo(unit, _rotationTarget);
        ApplyDamage(unit);
        CheckSwitchState(unit);
    }

    private void ApplyDamage(Unit unit)
    {
        if (_damageApplied)
        {
            return;
        }

        float fullDamage = unit.TargetUnit.Health.CalcDamage(unit.CurrentDamage, unit.TargetUnit.ArmorData.Value);
        bool isDamageTime = unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f;

        if (fullDamage > 0 && isDamageTime || fullDamage <= 0)
        {
            _damageApplied = true;
            unit.TargetUnit.Health.TakeDamage(fullDamage);
        }
        unit.TargetUnit.LastAttacker = unit;
    }

    private void RotateTo(Unit unit, Vector3 targetPosition)
    {
        Quaternion toRotation = Quaternion.LookRotation(targetPosition - unit.transform.position);
        if (Quaternion.Angle(unit.transform.rotation, toRotation) > 0.2f)
        {
            unit.transform.rotation = Quaternion.Lerp(unit.transform.rotation, toRotation,
                unit.Agent.speed * Time.deltaTime);
        }
        else
        {
            _rotationComplete = true;
        }
    }
}
