using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Outline), typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public class Unit : FSM
{    
    public enum CombatMode
    {
        Meele,
        Ranged
    };

    [SerializeField] private CombatMode _combatMode;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackValue = 15f;
    [SerializeField] private float _defenceValue = 15f;
    [SerializeField] private float _height = 2f;      
    [SerializeField] private UnitState _state;
    [SerializeField] private HeightModifier _heightModifier;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maximumMovementDistance = 10;
    private Team _team;
    private Selector _selector;
    private NavMeshObstacle _obstacle;
    private NavMeshAgent _agent;
    private Outline _outline;
    private float _currentMovementDistance;
    private float _currrentHeightModifer;
    private float _currentAttackValue;
    private float _currentDefenceValue;
    private Vector3 _lastPosition;
    private bool _hasFinishedActions = false;
    
    public UnitState State
    { 
        get => _state; 
        set => _state = value; 
    }
    public Selector Selector
    {
        get => _selector;
    }
    public Outline Outline
    {
        get => _outline;
    }
    public NavMeshAgent Agent
    {
        get => _agent;
    }
    public NavMeshObstacle Obstacle
    {
        get => _obstacle;
    }
    public Animator Animator
    {
        get => _animator;
    }
    public float CurrentMovementRange
    {
        get => _currentMovementDistance;
    }
    public Vector3 LastPosition
    {
        get => _lastPosition;
        set => _lastPosition = value;
    }
    public Team Team
    {
        get => _team;
    }
    public float AttackRange
    {
        get => _attackRange;
    }
    public float CurrentAttackValue
    {
        get => _currentAttackValue;
    }
    public float CurrentDefenceValue
    {
        get => _currentDefenceValue;
    }
    public CombatMode Combat
    {
        get => _combatMode;
    }
    public bool HasFinishedActions
    {
        get => _hasFinishedActions;
        set => _hasFinishedActions = value;
    }

    public void ChangeCurrentRange(float offset)
    {
        _currentMovementDistance += offset;
        if (_currentMovementDistance < 0)
        {
            _currentMovementDistance = 0;
        }
    }

    public void Initialize(Team team)
    {
        _team = team;
        _selector = team.Selector;
        _outline = GetComponent<Outline>();
        _obstacle = GetComponent<NavMeshObstacle>();
        _agent = GetComponent<NavMeshAgent>();
        _currentMovementDistance = _maximumMovementDistance;
        _state.EnterState(this);
    }

    public void NewTurn()
    {
        _currentMovementDistance = _maximumMovementDistance;
        _hasFinishedActions = false;
    }

    public void EndTurn()
    {
        _currentMovementDistance = 0;
    }

    private void Update()
    {
        _state.UpdateState(this);
        ApplyHeightModifier();
    }

    private void ApplyHeightModifier()
    {
        _currrentHeightModifer = _heightModifier.CalcualteModifier(transform.position.y, _height);
        _currentAttackValue = _attackValue * _currrentHeightModifer;
        _currentDefenceValue = _defenceValue * _currrentHeightModifer;
    }
}
