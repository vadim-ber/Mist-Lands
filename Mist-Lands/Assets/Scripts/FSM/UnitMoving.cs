using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Moving", menuName = "ScriptableObjects/FSM/Create moving state")]
public class UnitMoving : UnitState, IUnitHandler
{    
    public bool HasNewUnit { get; set; }

    public override void CheckSwitchState(Unit unit)
    {        
        if (!unit.Agent.pathPending)
        {  
            if (unit.Agent.remainingDistance <= unit.Agent.stoppingDistance)
            {               
                SwitchState(Transitions[0], unit);                
            }
        }        
        if (unit.Team.State is TeamNotSelected)
        {
            SwitchState(Transitions[1], unit);
        }
        if (HasNewUnit)
        {            
            HasNewUnit = false;
            SwitchState(Transitions[1], unit);
        }
        if (unit.CurrentMovementRange <= 0.05)
        {
            SwitchState(Transitions[0], unit);
        }
    }

    public override void EnterState(Unit unit)
    {        
        unit.Obstacle.enabled = false;
        unit.Agent.enabled = true;        
        unit.Selector.OnNewUnitSelected += HandleNewUnit;
        unit.LastPosition = unit.transform.position;
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
    }

    public override void ExitState(Unit unit)
    {        
        unit.Agent.enabled = false;
        unit.Selector.OnNewUnitSelected -= HandleNewUnit;
        unit.HasFinishedActions = true;        
    }

    public void HandleNewUnit(Unit unit)
    {
       HasNewUnit = true;
    }

    public override void UpdateState(Unit unit)
    {
        unit.Animator.SetFloat("Speed", unit.Agent.velocity.magnitude/unit.Agent.speed);
        Move(unit, unit.Selector.SelectedPosition);
        UpdateMovementDistance(unit);
        CheckSwitchState(unit);
    }

    private void Move(Unit unit, Vector3 newPosition)
    {
        unit.Agent.SetDestination(newPosition);
        NavMeshPath path = new();
        unit.Agent.CalculatePath(newPosition, path);
        float pathLength = path.CalculatePathLength();

        if (pathLength > unit.CurrentMovementRange)
        {
            Vector3 clampedPosition = path.GetPointAtDistance(unit.CurrentMovementRange);
            unit.Agent.SetDestination(clampedPosition);
        }
        else
        {
            unit.Agent.SetDestination(newPosition);
        }
    }

    private void UpdateMovementDistance(Unit unit)
    {
        float offset = Vector3.Distance(unit.transform.position, unit.LastPosition);
        unit.LastPosition = unit.transform.position;
        unit.ChangeCurrentRange(-offset);
    }
}
