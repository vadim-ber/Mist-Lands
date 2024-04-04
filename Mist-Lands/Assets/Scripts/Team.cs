using System.Collections.Generic;
using UnityEngine;

public class Team : FSM
{
    [SerializeField] private bool _aITeam;
    [SerializeField] private string _teamName;
    [SerializeField] private TeamState _state;
    [SerializeField] private List<Unit> _allUnits;
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
    public bool AITeam
    {
        get => _aITeam;  
    }

    public void StartNewTurn()
    {
        foreach (var unit in ActiveUnits)
        {
            unit.NewTurn();
        }
    }

    private void Start()
    {
        Initialize();       
    }

    private void Update()
    {
        State.UpdateState(this);
    }

    private void Initialize()
    {
        _activeUnits = new(_allUnits);
        foreach(Unit unit in _allUnits)
        {
            unit.SetTeam(this);
        }
        State.EnterState(this);
    }
}
