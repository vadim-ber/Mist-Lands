using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turner : MonoBehaviour
{
    [SerializeField] private Team[] _teams;
    [SerializeField] Text _text;
    private Queue<Team> _queue;
    private int _turnCount = 1;
    private int _teamsWasMoved = 0;
    private Team _currentTeam;

    public Team CurrentTeam
    {
        get => _currentTeam;
    }

    public void TurnEndButton()
    {
        if (_teams.Length < 1)
        {            
            return;
        }
        _currentTeam.EndCurrentTurn();
        TurnEnd();
    }

    private void Start()
    {
        Initialize();       
    }

    private void Initialize()
    {       
        if (_teams.Length < 1)
        {
            Debug.LogWarning("List of Teams is empty!");
            return;
        }
        _text.text = _turnCount.ToString();
        foreach (var team in _teams)
        {           
            team.EndCurrentTurn();
            team.SetTurner(this);
            team.OnTurnEnd += TurnEnd;
        }
        _queue = new Queue<Team>(_teams);
        _currentTeam = _queue.Dequeue();
        _currentTeam.StartNewTurn();        
    }

    private void TurnEnd()
    {
        _queue.Enqueue(_currentTeam);
        _currentTeam = _queue.Dequeue();
        _currentTeam.StartNewTurn();

        _teamsWasMoved++;

        if (_teamsWasMoved >= _teams.Length)
        {
            _teamsWasMoved = 0;
            _turnCount++;
            _text.text = _turnCount.ToString();
        }
    }

    private void OnDisable()
    {
        foreach (var team in _teams)
        {           
            team.OnTurnEnd -= TurnEnd;
        }
    }
}
