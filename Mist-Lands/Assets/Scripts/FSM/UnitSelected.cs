using UnityEngine;

[CreateAssetMenu(fileName = "Selected", menuName = "ScriptableObjects/FSM/Create Selected state")]
public class UnitSelected : UnitState, IUnitHandler, INewVectorHandler
{
    private UnitFInderHelper _helper;
    public bool HasNewUnit { get; set; }
    public bool HasNewVector { get; set; }

    public override void CheckSwitchState(Unit unit)
    {
        if (HasNewUnit)
        {
            HasNewUnit = false;
            SwitchState(Transitions[0], unit);
        }
        else if (unit.Team.State is TeamNotSelected)
        {
            SwitchState(Transitions[0], unit);
        }
        else if (HasNewVector)
        {            
            HasNewVector = false;
            SwitchState(Transitions[1], unit);
        }                
    }

    public override void EnterState(Unit unit)
    {
        _helper = new();
        HasNewVector = false;
        unit.Outline.enabled = true;
        unit.Selector.OnNewPositionSelected += HandleNewVector;
        unit.Selector.OnNewUnitSelected += HandleNewUnit;
        if (unit.Animator == null)
        {
            return;
        }
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
    }

    public override void ExitState(Unit unit)
    {
        unit.Selector.OnNewPositionSelected -= HandleNewVector;
        unit.Selector.OnNewUnitSelected -= HandleNewUnit;
        if (unit.Animator == null)
        {
            return;
        }        
    }    

    public override void UpdateState(Unit unit)
    {
        CheckSwitchState(unit);
        _helper.FindUnitsInContact(unit);
    }

    public void HandleNewUnit(Unit unit)
    {
        HasNewUnit = true;
    }

    public void HandleNewVector(Vector3 newVector)
    {        
        HasNewVector = true;
    }
}
