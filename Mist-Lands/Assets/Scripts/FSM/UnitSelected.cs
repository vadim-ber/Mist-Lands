using UnityEngine;

[CreateAssetMenu(fileName = "Selected", menuName = "ScriptableObjects/FSM/Create Selected state")]
public class UnitSelected : UnitState, IUnitHandler, INewVectorHandler
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
        else if (_attackInvoked && unit.CurrentActionPoints >= unit.Weapon.AttackPrice)
        {  
            SwitchState(Transitions[2], unit);
            _attackInvoked = false;
        }
        else if (HasNewUnit)
        {
            HasNewUnit = false;
            SwitchState(Transitions[0], unit);
        }
        else if (unit.Team.State is TeamNotSelected)
        {
            SwitchState(Transitions[0], unit);
        }
    }

    public override void EnterState(Unit unit)
    {        
        HasNewVector = false;
        unit.Obstacle.enabled = false;
        unit.Outline.enabled = true;
        unit.Selector.OnNewPositionSelected += HandleNewVector;
        unit.Selector.OnNewUnitSelected += HandleNewUnit;
        unit.Selector.OnAttackIsPossible += HandleAttack;
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
        unit.Animator.SetFloat("Speed", 0f);
    }

    public override void ExitState(Unit unit)
    {
        unit.Selector.OnNewPositionSelected -= HandleNewVector;
        unit.Selector.OnNewUnitSelected -= HandleNewUnit;
        unit.Selector.OnAttackIsPossible -= HandleAttack;
    }

    public override void UpdateState(Unit unit)
    {
        CheckSwitchState(unit);
    }

    public void HandleNewUnit(Unit unit)
    {
        HasNewUnit = true;
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
