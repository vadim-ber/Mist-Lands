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

    public void TurnEnd()
    {
        if (_teams.Length == 0)
        {
            return;
        }

        foreach (var team in _teams)
        {
            team.StartNewTurn();
        }

        _teamsWasMoved++;

        if (_teamsWasMoved >= _teams.Length)
        {
            _teamsWasMoved = 0;
            _turnCount++;
            _text.text = _turnCount.ToString();
        }
    }

    private void Start()
    {
        Initialize();       
    }

    private void Initialize()
    {
        _text.text = _turnCount.ToString();
        foreach (var team in _teams)
        {
            team.State = Resources.Load<TeamNotSelected>("ScriptableObjects/FSM");
        }
        _queue = new Queue<Team>(_teams);
    }
}
