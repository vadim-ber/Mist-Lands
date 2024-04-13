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
        return _unit.Combat switch
        {
            Unit.CombatMode.Meele => HandleMeeleCombat(),
            Unit.CombatMode.Ranged => HandleRangedCombat(),
            _ => Vector3.zero,
        };
    }

    private Vector3 HandleMeeleCombat()
    {
        switch (_unit.AI)
        {
            case Unit.AIGrade.Stupid:
            case Unit.AIGrade.Normal:
               // или HelpingToAlly()
               // Retreat() возможен
               // HoldPosition() возможен;
            case Unit.AIGrade.Smart:
                // TakeAdvantagePosition()
                // или HelpingToAlly()
                // BreakContact() возможен
                // Retreat() возможен
                // HoldPosition() возможен;
                return Convergence();
            default:
                return Vector3.zero;
        }
    }

    private Vector3 HandleRangedCombat()
    {
        switch (_unit.AI)
        {
            case Unit.AIGrade.Stupid:
                // BreakContact() возможен
                // Retreat() возможен
                //HoldPosition();
                return BreakContact();
            case Unit.AIGrade.Normal:
                // BreakContact() возможен
                // SwitchCombatMode() возможен (при сближении)
                // Retreat() возможен
                return HoldPosition();
            case Unit.AIGrade.Smart:
                // BreakContact() возможен
                // SwitchCombatMode() возможен (при сближении)
                // TakeAdvantagePosition() возможен
                // Retreat() возможен
                return HoldPosition();
            default:
                return Vector3.zero;
        }
    }

    private Vector3 HoldPosition()
    {
        // если позицию перед атакой нельзя улучшить, или это слишком
        // рискованно
        Debug.LogWarning($"{_unit.name} удерживает позицию");
        _unit.HasFinishedActions = true;
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
        var enemies = _unit.GetUnitsInRadius(_unit.Agent.radius, false);
        if(enemies.Count < 1)
        {
            Vector3 result = FindAvailablePosition(_unit, enemies, _unit.Agent.radius * 2,
                _unit.Agent.radius);
            return result;
        }
        else
        {
            return _unit.transform.position;
        }
    }

    private Vector3 TakeAdvantagePosition()
    {
        // занять выгодную позицию (вершину) в радиусе, позволяющем атаковать противника
        // в meele, или range
        return _unit.transform.position;
    }

    private Vector3 Retreat()
    {
        // двигаться в направлении от противника в случае
        // если он слишком силен, или получены значительные повреждения
        // аналогично Convergence, только движение в противоположную сторону
        return _unit.transform.position;
    }

    private Vector3 HelpingToAlly()
    {
        // искать в радиусе хода союзника, находящегося в контакте с противником
        // если их несколько выбирать :
                //самого слабого союзника, если противник с ним в контакте примерно
                //равен по силе текущему юниту, или слабее

                //самого сильного союзника, если противник с ним в контакте
                //сильнее текущего юнита
        return _unit.transform.position;
    }

    private void SwitchCombatMode()
    {
        // меняем боевой режим _unit
    }

    private Vector3 FindAvailablePosition(Unit unit, List<Unit> allUnits,
        float searchRadius, float stepSize)
    {
        for (float x = unit.transform.position.x - searchRadius;
            x <= unit.transform.position.x + searchRadius; x += stepSize)
        {
            for (float z = unit.transform.position.z - searchRadius;
                z <= unit.transform.position.z + searchRadius; z += stepSize)
            {
                Vector3 potentialPosition = new(x, unit.transform.position.y, z);
                bool isAvailable = true;

                foreach (Unit otherUnit in allUnits)
                {
                    if (otherUnit == unit)
                        continue;

                    float distance = Vector3.Distance(otherUnit.transform.position,
                        potentialPosition);
                    if (distance <= otherUnit.Agent.radius)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable)
                    return potentialPosition;
            }
        }
        return Vector3.zero; 
    }
}
