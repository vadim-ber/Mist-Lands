using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    public delegate void UnitsContactDelegate(Unit unit, Unit[] unitsInContact);

    public static event UnitsContactDelegate OnUnitsContact;

    private List<Team> _teams;
    private Dictionary<GameObject, Unit> _allUnitsDictonary = new();

    public Dictionary<GameObject, Unit> AllUnitsDictonary
    {
        get => _allUnitsDictonary;
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

        if (_teams.Count > 0)
        {
            foreach (var team in _teams)
            {
                if(team.AllUnits.Count > 0)
                {
                    foreach (var unit in team.AllUnits)
                    {
                        _allUnitsDictonary.Add(unit.gameObject, unit);
                    }
                }               
            }
        }
        else
        {
            Debug.LogWarning("teams not founded!");
        }
    }
}
