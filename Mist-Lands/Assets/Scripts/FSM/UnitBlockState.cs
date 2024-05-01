using UnityEngine;

[CreateAssetMenu(fileName = "UnitBlockState", menuName = "ScriptableObjects/FSM/Create UnitBlockState")]
public class UnitBlockState : UnitState
{
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            SwitchState((UnitState)Transitions.List[0], unit);
        }
    }

    public override void EnterState(Unit unit)
    {
        unit.Animator.StopPlayback();
        unit.Animator.SetFloat("Context", Random.Range(0, 2));
        unit.Animator.Play(CurrentStateAnimationName);
        unit.Health.BlockPossible = false;
    }

    public override void ExitState(Unit unit)
    {
        unit.Animator.SetFloat("Context", 0f);
    }

    public override void UpdateState(Unit unit)
    {
        CheckSwitchState(unit);
    }
}
