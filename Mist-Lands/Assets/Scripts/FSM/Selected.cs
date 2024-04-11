using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Selected", menuName = "ScriptableObjects/FSM/Create Selected state")]
public class Selected : UnitState, IUnitHandler, INewVectorHandler
{    
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
            float distance = Vector3.Distance(unit.transform.position, unit.Selector.SelectedPosition);
            if (unit.CurrentMovementRange > 0.1 && distance > unit.Agent.radius)
            {
                HasNewVector = false;
                SwitchState(Transitions[1], unit);
            }
        }                
    }

    public override void EnterState(Unit unit)
    {
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
