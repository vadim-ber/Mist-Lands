using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Outline), typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public class Unit : FSM
{
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;
    [SerializeField] private UnitState _state;
    [SerializeField] private HeightModifier _heightModifier;
    [SerializeField] private WeaponSlotsHandler _weaponSlotsHandler;    
    [SerializeField] private float _maximumMovementDistance = 10;
    [SerializeField] private int _maximumActionPoints = 2;
    [SerializeField] private float _height = 2f;
    private Team _team;
    private Selector _selector;    
    private NavMeshObstacle _obstacle;
    private NavMeshAgent _agent;
    private Outline _outline;
    private UnitFinder _unitFinder;
    private List<Unit> _findedUnits;
    private Unit _targetUnit;
    private Unit _lastAttacker;
    private float _currentMovementDistance;
    private int _currentActionPoints;
    private float _currrentHeightModifer;
    private float _currentAttackRange;
    private float _currentDamage;
    private Vector3 _lastPosition;
    private bool _pathIsCompleted = false;
    private bool _attacksIsPossible = true;


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
    public Health Health
    {
        get => _health;
    }
    public Animator Animator
    {
        get => _animator;
    }
    public List<Unit> FindedUnits
    {
        get => _findedUnits;
    }
    public Unit TargetUnit
    {
        get => _targetUnit;
        set => _targetUnit = value;
    }
    public Unit LastAttacker
    {
        get => _lastAttacker;
        set => _lastAttacker = value;
    }
    public float CurrentMovementRange
    {
        get => _currentMovementDistance;
    }
    public int CurrentActionPoints
    {
        get => _currentActionPoints;
        set => _currentActionPoints = value;
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
    public float CurrentAttackRange
    {
        get => _currentAttackRange;
    }
    public float CurrentDamage
    {
        get => _currentDamage;
    }
    public Weapon Weapon
    {
        get => _weapon;
    }
    public Armor Armor
    {
        get => _armor;
    }
    public bool PathIsCompleted
    {
        get => _pathIsCompleted;
        set => _pathIsCompleted = value;
    }
    public bool AttacksIsPossible
    {
        get => _attacksIsPossible;
        set => _attacksIsPossible = value;
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
        _animator.runtimeAnimatorController = _weapon.AnimatorController;
        _team = team;
        _selector = team.Selector;
        _unitFinder = new(this, team.Selector.UnitList.AllUnitsDictonary);
        _findedUnits = new();
        _weapon.Initialize(_weaponSlotsHandler);
        _outline = GetComponent<Outline>();
        _obstacle = GetComponent<NavMeshObstacle>();
        _agent = GetComponent<NavMeshAgent>();        
        _currentMovementDistance = _maximumMovementDistance;
        _currentActionPoints = _maximumActionPoints;
        _state.EnterState(this);
    }

    public void NewTurn()
    {
        _currentMovementDistance = _maximumMovementDistance;
        _currentActionPoints = _maximumActionPoints;
        _pathIsCompleted = false;
        _attacksIsPossible = true;
    }

    public void EndTurn()
    {
        _currentMovementDistance = 0;
    }

    private void Update()
    {
        _state.UpdateState(this);
        ApplyHeightModifier();
        if(_state is not UnitNotSelected)
        {
            _findedUnits = _unitFinder.FindUnitsInRadius(CurrentAttackRange, false);
        }
    }

    private void ApplyHeightModifier()
    {
        _currrentHeightModifer = _heightModifier.CalcualteModifier(transform.position.y, _height);
        _currentAttackRange = Weapon.AttackRange * _currrentHeightModifer;
        _currentDamage = Weapon.Damage * _currrentHeightModifer;
    }
}
