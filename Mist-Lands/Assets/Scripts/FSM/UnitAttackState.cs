using UnityEngine;

[CreateAssetMenu(fileName = "UnitAttackState", menuName = "ScriptableObjects/FSM/Create UnitAttackState")]
public class UnitAttackState : UnitState
{
    [SerializeField] float _attackTimer = 2f;
    private float _attackTime = 0f;
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            return;
        }
        if (unit.Team.Mode is Team.TeamMode.AIControlled && unit.CurrentActionPoints
            >= unit.Weapon.AttackPrice)
        {
            SwitchState(Transitions[1], unit);
        }
        else
        {
            SwitchState(Transitions[0], unit);
        }
    }

    public override void EnterState(Unit unit)
    {
        unit.StartCoroutine(unit.WaitRotationTo(unit.TargetUnit.transform.position));
        _attackTime = 0f;
        unit.Obstacle.enabled = false;
        unit.Agent.enabled = false;
        Debug.Log($"{unit.name} начинает атаку {unit.TargetUnit.name}");        
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
        _attackTime += Time.deltaTime;
    }
}
