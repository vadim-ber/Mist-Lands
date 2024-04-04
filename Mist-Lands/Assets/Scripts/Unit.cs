using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Outline), typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public class Unit : FSM
{
    //ScriptableObject InputMode - player/AI - children of one base class
    [SerializeField] private float _attackValue = 15f;
    [SerializeField] private float _defenceValue = 15f;
    [SerializeField] private float _height = 2f;
    [SerializeField] private Clicker _clicker;   
    [SerializeField] private UnitState _state;
    [SerializeField] private HeightModifier _heightModifier;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maximumMovementDistance = 10;
    private Team _team;
    private NavMeshObstacle _obstacle;
    private NavMeshAgent _agent;
    private Outline _outline;
    private float _currentMovementDistance;
    private float _currrentHeightModifer;
    private float _currentAttackValue;
    private float _currentDefenceValue;
    private Vector3 _lastPosition;
    public UnitState State
    { 
        get => _state; 
        set => _state = value; 
    }
    public Clicker Clicker
    {
        get => _clicker;
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

    public void ChangeCurrentRange(float offset)
    {
        _currentMovementDistance += offset;
        if (_currentMovementDistance < 0)
        {
            _currentMovementDistance = 0;
        }
    }

    public void SetTeam(Team team)
    {
        _team = team;
    }

    public void NewTurn()
    {
        _currentMovementDistance = _maximumMovementDistance;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        _state.UpdateState(this);
        ApplyHeightModifier();    
    }

    private void Initialize()
    {
        _outline = GetComponent<Outline>();
        _obstacle = GetComponent<NavMeshObstacle>();
        _agent = GetComponent<NavMeshAgent>();
        _currentMovementDistance = _maximumMovementDistance;
        _state.EnterState(this);
    }

    private void ApplyHeightModifier()
    {
        _currrentHeightModifer = _heightModifier.CalcualteModifier(transform.position.y, _height);
        _currentAttackValue = _attackValue * _currrentHeightModifer;
        _currentDefenceValue = _defenceValue * _currrentHeightModifer;
    }
}
