using UnityEngine;

[CreateAssetMenu(fileName = "NotSelected", menuName = "ScriptableObjects/FSM/Create NOT Selected state")]
public class NotSelected : State, IUnitHandler
{
    public bool HasNewUnit { get; set; }

    public override void CheckSwitchState(Unit unit)
    {
        if(HasNewUnit && unit.Clicker.SelectedUnit == unit)
        {
            HasNewUnit = false;
            SwitchState(Transitions[0], unit);
        }
    }        

    public override void EnterState(Unit unit)
    {
        unit.Outline.enabled = false;
        unit.Agent.enabled = false;
        unit.Clicker.OnUnitChanged += HandleNewUnit;
        unit.Obstacle.enabled = true;
        if (unit.Animator == null)
        {
            return;
        }
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
    }

    public override void ExitState(Unit unit)
    {
        unit.Clicker.OnUnitChanged -= HandleNewUnit;
        unit.Obstacle.enabled = false;
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
}
