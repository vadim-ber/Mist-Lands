using UnityEngine;

[CreateAssetMenu(fileName = "Selected", menuName = "ScriptableObjects/FSM/Create Selected state")]
public class UnitSelected : UnitState, INewVectorHandler
{   
    public bool HasNewUnit { get; set; }
    public bool HasNewVector { get; set; }
    private bool _attackInvoked;

    public override void CheckSwitchState(Unit unit)
    {
        if (HasNewVector)
        {           
            HasNewVector = false;
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
            _attackInvoked = false;
        }
        if (unit.Team.Mode is Team.TeamMode.PlayerControlled
            && _attackInvoked && unit.CurrentActionPoints >= unit.Weapon.AttackPrice)
        {           
            _attackInvoked = false;
            SwitchState(Transitions[2], unit);
        }
        if (unit.Selector.SelectedUnit != unit)
        {
            HasNewUnit = false;
            SwitchState(Transitions[0], unit);
        }
        if (unit.Team.State is TeamNotSelected)
        {
            SwitchState(Transitions[0], unit);
        }        
    }

    public override void EnterState(Unit unit)
    {        
        HasNewVector = false;
        HasNewUnit = false;
        _attackInvoked = false;
        unit.Obstacle.enabled = false;
        unit.Outline.enabled = true;
        unit.Selector.OnNewPositionSelected += HandleNewVector;
        unit.Selector.OnAttackIsPossible += HandleAttack;
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
        unit.Animator.SetFloat("Speed", 0f);
    }

    public override void ExitState(Unit unit)
    {
        unit.Selector.OnNewPositionSelected -= HandleNewVector;
        unit.Selector.OnAttackIsPossible -= HandleAttack;
    }

    public override void UpdateState(Unit unit)
    {
        CheckSwitchState(unit);
    }

    public void HandleNewVector(Vector3 newVector)
    {        
        HasNewVector = true;
    }

    private void HandleAttack(Unit[] units)
    {        
        _attackInvoked = true;
    }
}
