using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    public delegate void UnitsContactDelegate(Unit unit, Unit[] unitsInContact);

    public static event UnitsContactDelegate OnUnitsContact;

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

    public static void InvokeOnUnitsContact(Unit unit, Unit[] array)
    {
        OnUnitsContact?.Invoke(unit, array);        
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
