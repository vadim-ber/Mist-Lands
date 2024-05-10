using UnityEngine;

[CreateAssetMenu(fileName = "UnitUnsheathWeapon", menuName = "ScriptableObjects/FSM/Create UnitUnsheathWeapon")]
public class UnitUnsheathW : UnitState
{
    private bool _isComplete;
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {            
            SwitchState((UnitState)Transitions.List[1], unit);
        }
    }

    public override void EnterState(Unit unit)
    {
        _isComplete = false; 
        unit.Animator.StopPlayback();
        unit.Animator.Play(CurrentStateAnimationName, AnimationLayer);
    }

    public override void ExitState(Unit unit)
    {
        unit.Animator.StopPlayback();
    }

    public override void UpdateState(Unit unit)
    {
        WaitToAnimation(unit);
        CheckSwitchState(unit);
    }

    private void WaitToAnimation(Unit unit)
    {
        if (_isComplete)
        {
            return;
        }

        bool isUnsheathTime = unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.10f;

        if (isUnsheathTime)
        {
            _isComplete = true;
            unit.WeaponInHands.Unsheath();
        }
    }
}
