using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Outline), typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public class Unit : FSM
{
    [SerializeField] private RigBuilder _rigBuilder;
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<WeaponData> _weaponDataList;
    [SerializeField] private ArmorData _armorData;
    [SerializeField] private UnitState _state;
    [SerializeField] private HeightModifier _heightModifier;
    [SerializeField] private CharacterEquipmentSlots _characterEquipmentSlots;    
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
    private Weapon _weaponInHands;
    private List<Weapon> _equippiedWeapons;
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
    public Selector Selector => _selector;    
    public Outline Outline => _outline;
    public NavMeshAgent Agent => _agent;
    public NavMeshObstacle Obstacle => _obstacle;
    public Health Health => _health;
    public Animator Animator => _animator;
    public List<Unit> FindedUnits => _findedUnits;    
    public float CurrentMovementRange => _currentMovementDistance;    
    public Team Team => _team;
    public float CurrentAttackRange => _currentAttackRange;
    public float CurrentDamage => _currentDamage;
    public Weapon WeaponInHands => _weaponInHands;
    public ArmorData ArmorData => _armorData;    

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
        _equippiedWeapons = new()
        {
            new(_weaponDataList[0], _characterEquipmentSlots),
            new(_weaponDataList[1], _characterEquipmentSlots)
        };
        _equippiedWeapons[0].Unsheath();
        _weaponInHands = _equippiedWeapons[0];
        _animator.runtimeAnimatorController = _weaponInHands.WeaponData.AnimatorController;
        _team = team;
        _selector = team.Selector;
        _unitFinder = new(this, team.Selector.UnitList.AllUnitsDictonary);
        _findedUnits = new();        
        _rigBuilder.Build();
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

    public void SwapWeapon()
    {
        _weaponInHands.Sheath();
        _weaponInHands = _equippiedWeapons.FirstOrDefault(obj => obj != _weaponInHands);
        _animator.runtimeAnimatorController = _weaponInHands.WeaponData.AnimatorController;
        _weaponInHands.Unsheath();
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
        _currentAttackRange = _weaponInHands.WeaponData.AttackRange * _currrentHeightModifer;
        _currentDamage = _weaponInHands.WeaponData.Damage * _currrentHeightModifer;
    }    
}
