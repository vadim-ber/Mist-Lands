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
               // ��� HelpingToAlly()
               // Retreat() ��������
               // HoldPosition() ��������;
            case Unit.AIGrade.Smart:
                // TakeAdvantagePosition()
                // ��� HelpingToAlly()
                // BreakContact() ��������
                // Retreat() ��������
                // HoldPosition() ��������;
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
                // BreakContact() ��������
                // Retreat() ��������
                return HoldPosition();
            case Unit.AIGrade.Normal:
                // BreakContact() ��������
                // SwitchCombatMode() �������� (��� ���������)
                // Retreat() ��������
                return HoldPosition();
            case Unit.AIGrade.Smart:
                // BreakContact() ��������
                // SwitchCombatMode() �������� (��� ���������)
                // TakeAdvantagePosition() ��������
                // Retreat() ��������
                return HoldPosition();
            default:
                return Vector3.zero;
        }
    }

    private Vector3 HoldPosition()
    {
        // ���� ������� ����� ������ ������ ��������, ��� ��� �������
        // ����������
        Debug.LogWarning($"{_unit.name} ���������� �������");
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
        // ���� ��������� � �������� ������� �����, �� ���� ����� ������������ ���������
        // c ��� �������
        return _unit.transform.position;
    }

    private Vector3 TakeAdvantagePosition()
    {
        // ������ �������� ������� (�������) � �������, ����������� ��������� ����������
        // � meele, ��� range
        return _unit.transform.position;
    }

    private Vector3 Retreat()
    {
        // ��������� � ����������� �� ���������� � ������
        // ���� �� ������� �����, ��� �������� ������������ �����������
        // ���������� Convergence, ������ �������� � ��������������� �������
        return _unit.transform.position;
    }

    private Vector3 HelpingToAlly()
    {
        // ������ � ������� ���� ��������, ������������ � �������� � �����������
        // ���� �� ��������� �������� :
                //������ ������� ��������, ���� ��������� � ��� � �������� ��������
                //����� �� ���� �������� �����, ��� ������

                //������ �������� ��������, ���� ��������� � ��� � ��������
                //������� �������� �����
        return _unit.transform.position;
    }

    private void SwitchCombatMode()
    {
        // ������ ������ ����� _unit
    }
}
