using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    private List<Team> _teams;
    private List<Unit> _allUnitsList;

    public List<Unit> AllUnitsList
    {
        get => _allUnitsList;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _teams = FindObjectsByType<Team>(FindObjectsSortMode.None).ToList();
        _allUnitsList = new List<Unit>();

        if (_teams.Count > 0)
        {
            foreach (var team in _teams)
            {
                _allUnitsList.AddRange(team.AllUnits);
            }
        }
        else
        {
            Debug.LogWarning("teams not founded!");
        }
    }
}
