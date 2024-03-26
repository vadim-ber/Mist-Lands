using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Outline), typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public class Unit : MonoBehaviour
{
    [SerializeField] private Clicker _clicker;   
    [SerializeField] private State _state;
    [SerializeField] private Animator _animator;
    private NavMeshObstacle _obstacle;
    private NavMeshAgent _agent;
    private Outline _outline;
    public State State
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

    private void Start()
    {
        Initialize();
        _state.EnterState(this);
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
    }
}
