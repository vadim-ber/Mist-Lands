using UnityEngine;

[CreateAssetMenu(fileName = "Moving", menuName = "ScriptableObjects/FSM/Create moving state")]
public class Moving : State, IUnitHandler
{
    public bool HasNewUnit { get; set; }

    public override void CheckSwitchState(Unit unit)
    {
        if (!unit.Agent.pathPending)
        {
            if (unit.Agent.remainingDistance <= unit.Agent.stoppingDistance)
            {
                if (!unit.Agent.hasPath || unit.Agent.velocity.sqrMagnitude == 0f)
                {
                    SwitchState(Transitions[0], unit);
                }
            }
        }
        if(HasNewUnit)
        {
            HasNewUnit = false;
            SwitchState(Transitions[1], unit);
        }
    }

    public override void EnterState(Unit unit)
    {        
        unit.Agent.enabled = true;
        unit.Clicker.OnUnitChanged += HandleNewUnit;
        if (unit.Animator == null)
        {
            return;
        }
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
    }

    public override void ExitState(Unit unit)
    {
        unit.Agent.ResetPath();
        unit.Agent.enabled = false;
        unit.Clicker.OnUnitChanged -= HandleNewUnit;
        if (unit.Animator == null)
        {
            return;
        }
        unit.Animator.SetFloat("AgentVelocity", 0.3f);
    }

    public void HandleNewUnit(Unit unit)
    {
       HasNewUnit = true;
    }

    public override void UpdateState(Unit unit)
    {
        unit.Animator.SetFloat("AgentVelocity", unit.Agent.velocity.magnitude/unit.Agent.speed);
        Move(unit, unit.Clicker.ClickedPosition);
        CheckSwitchState(unit);          
    }

    private void Move(Unit unit, Vector3 newPosition)
    {
        unit.Agent.SetDestination(newPosition);
    }
}
