using System.Collections.Generic;
using UnityEngine;

public class Team : FSM
{    
    [SerializeField] private string _teamName;
    [SerializeField] private Selector _selector;
    [SerializeField] private TeamState _state;
    [SerializeField] private List<Unit> _allUnits;
    private Turner _turner;
    private List<Unit> _activeUnits;

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
        set
        {
            _selector = value;
            SwitchSelector();
        }
    }

    public void StartNewTurn()
    {
        foreach (var unit in ActiveUnits)
        {
            unit.NewTurn();
        }
    }

    public void EndCurrentTurn()
    {
        foreach (var unit in ActiveUnits)
        {
            unit.EndTurn();
        }
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
    }

    private void Initialize()
    {
        _activeUnits = new(_allUnits);
        SwitchSelector();
        State.EnterState(this);
    }

    private void SwitchSelector()
    {
        _selector.Initialize(transform);
        foreach (Unit unit in _allUnits)
        {
            unit.Initialize(this);
        }
    }
}
