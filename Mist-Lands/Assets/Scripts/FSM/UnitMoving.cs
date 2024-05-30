using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Moving", menuName = "ScriptableObjects/FSM/Create moving state")]
public class UnitMoving : UnitState
{
    private NavMeshPath _path;
    private float _pathLength;
    private Vector3 _clampedPosition;
    private float _offset;
    public override void CheckSwitchState(Unit unit)
    {        
        if (!unit.Agent.pathPending)
        {  
            if (unit.Agent.remainingDistance <= unit.Agent.stoppingDistance)
            {
                SwitchState((UnitState)Transitions.List[1], unit);                
            }
        }        
        if (unit.Team.State is TeamNotSelected)
        {
            SwitchState((UnitState)Transitions.List[0], unit);
        }
        if (unit.Selector.SelectedUnit != unit)
        { 
            SwitchState((UnitState)Transitions.List[0], unit);
        }
        if (unit.CurrentMovementRange <= 0.05)
        {
            SwitchState((UnitState)Transitions.List[1], unit);
        }
    }

    public override void EnterState(Unit unit)
    {
        _path = new();
        _pathLength = 0;
        unit.Obstacle.enabled = false;
        unit.Agent.enabled = true; 
        unit.LastPosition = unit.transform.position;
        unit.Selector.HasNewSelectedPosition = false;
        unit.Animator.CrossFade(CurrentStateAnimationName, AnimationTrasitionTime);
    }

    public override void ExitState(Unit unit)
    {        
        unit.Agent.enabled = false;
        unit.PathIsCompleted = true;
        unit.Selector.HasNewSelectedPosition = false;
        unit.Animator.StopPlayback();
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
        unit.Agent.CalculatePath(newPosition, _path);
        _pathLength = _path.CalculatePathLength();

        if (_pathLength > unit.CurrentMovementRange)
        {
            _clampedPosition = _path.GetPointAtDistance(unit.CurrentMovementRange);
            unit.Agent.SetDestination(_clampedPosition);
        }
        else
        {
            unit.Agent.SetDestination(newPosition);
        }
    }

    private void UpdateMovementDistance(Unit unit)
    {
        _offset = Vector3.Distance(unit.transform.position, unit.LastPosition);
        unit.LastPosition = unit.transform.position;
        unit.ChangeCurrentRange(-_offset);
    }
}
