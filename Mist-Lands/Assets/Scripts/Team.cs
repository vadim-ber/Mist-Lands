using System;
using System.Collections.Generic;
using UnityEngine;

public class Team : FSM
{    
    public enum TeamMode
    {
        PlayerControlled,
        AIControlled
    };

    [SerializeField] private TeamMode _mode;
    [SerializeField] private string _teamName;
    [SerializeField] private Selector _selector;
    [SerializeField] private TeamState _state;
    [SerializeField] private List<Unit> _allUnits;
    [Header("PathDrawer Settings")]
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _waypointPrefab;
    private Turner _turner;
    private List<Unit> _activeUnits;
    private PathDrawer _pathDrawer;
    public event Action OnTurnStart;
    public event Action OnTurnEnd;

    public string TeamName
    {
        get =>_teamName;
    }
    public TeamState State
    {
        get => _state;
        set => _state = value;
    }
    public List<Unit> AllUnits
    {
        get => _allUnits;
    }
    public List<Unit> ActiveUnits
    {
        get => _activeUnits;
    }
    public Turner Turner
    {
        get => _turner;
    }
    public Selector Selector
    {
        get => _selector;        
    }

    public void StartNewTurn()
    {
        foreach (var unit in ActiveUnits)
        {
            unit.NewTurn();
        }
        OnTurnStart?.Invoke();
    }

    public void EndCurrentTurn()
    {
        foreach (var unit in ActiveUnits)
        {
            unit.EndTurn();
        }
        OnTurnEnd?.Invoke();
    }

    public void SetTurner(Turner turner)
    {
        _turner = turner;
    }

    private void Awake()
    {
        Initialize();       
    }

    private void Update()
    {
        State.UpdateState(this);
        _selector.UpdateSelector(transform);
        _pathDrawer.UpdatePath(transform);
    }

    private void Initialize()
    {        
        _activeUnits = new(_allUnits);
        CheckTeamMode();
        _pathDrawer = ScriptableObject.CreateInstance<PathDrawer>();
        _pathDrawer.Initialize(_selector, transform, _linePrefab, _waypointPrefab);
        _state.EnterState(this);
        foreach (var unit in _activeUnits)
        {
            unit.Initialize(this);
        }
    }
    
    private void CheckTeamMode()
    {
        switch(_mode)
        {
            case TeamMode.PlayerControlled:
                _selector = new Clicker(this);                
                break;

            case TeamMode.AIControlled: 
                _selector = new AISelector(this);
                break;
        }
    }

    private void OnDisable()
    {
        _selector.StopListening();
    }
}
