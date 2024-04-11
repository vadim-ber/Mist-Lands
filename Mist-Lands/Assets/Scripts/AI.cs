using System.Collections.Generic;
using UnityEngine;

public class AI 
{
    private Unit _unit;
    private List<Unit> _allUnits;
    private const float _maxRange = 10000f;
    public AI(Unit unit, List<Unit> allUnits)
    {
        _unit = unit;
        _allUnits = allUnits;
    }
    public Vector3 CalcVectorToMove()
    {
        if(_unit.Combat == Unit.CombatMode.Meele)
        {
            if(_unit.AI == Unit.AIGrade.Stupid)
            {
                return Convergence();
            }
            if (_unit.AI == Unit.AIGrade.Normal)
            {
                return Convergence();
            }
            if (_unit.AI == Unit.AIGrade.Smart)
            {
                return Convergence();
            }
        }
        if(_unit.Combat == Unit.CombatMode.Ranged)
        {
            return Vector3.zero;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 HoldPosition()
    {
        return _unit.transform.position;
    }

    private Vector3 Convergence()
    {
        float minDistance = _maxRange;
        Unit closestUnit = null;    
        foreach(Unit unit in _allUnits)
        {
            if(unit.Team != _unit.Team)
            {
                float dist = Vector3.Distance(unit.transform.position,
                    _unit.transform.position);
                if(dist < minDistance)
                {
                    minDistance = dist;
                    closestUnit = unit;
                }
            }
        }
        if (closestUnit != null)
        {
            return closestUnit.transform.position;
        }
        else
        {
            Debug.LogWarning("No unit found!");
            return Vector3.zero; 
        }
    }

    private Vector3 BreakContact()
    {
        return _unit.transform.position;
    }

    private Vector3 TakeAdvantagePosition()
    {
        return _unit.transform.position;
    }

    private Vector3 Retreat()
    {
        return _unit.transform.position;
    }

    private Vector3 HelpingToAlly()
    {
        return _unit.transform.position;
    }
}
