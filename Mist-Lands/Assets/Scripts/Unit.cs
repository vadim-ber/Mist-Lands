using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Outline), typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public class Unit : FSM
{
    [SerializeField] private Clicker _clicker;   
    [SerializeField] private UnitState _state;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maximumMovementDistance = 10;
    private NavMeshObstacle _obstacle;
    private NavMeshAgent _agent;
    private Outline _outline;
    private float _currenMovementRange;
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
        get => _currenMovementRange;
    }
    public Vector3 LastPosition
    {
        get => _lastPosition;
        set => _lastPosition = value;
    }

    public void ChangeCurrentRange(float offset)
    {
        _currenMovementRange += offset;
        if (_currenMovementRange < 0)
        {
            _currenMovementRange = 0;
        }
    }

    public void NewTurn()
    {
        _currenMovementRange = _maximumMovementDistance;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        _state.UpdateState(this);
    }

    private void Initialize()
    {
        _outline = GetComponent<Outline>();
        _obstacle = GetComponent<NavMeshObstacle>();
        _agent = GetComponent<NavMeshAgent>();
        _currenMovementRange = _maximumMovementDistance;
        _state.EnterState(this);
    }
}
