using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterEquipmentSlots : MonoBehaviour
{
    [Header("Bow")]
    [SerializeField] private Transform _bowHandSlot;
    [SerializeField] private Transform _bowBackSlot;
    [SerializeField] private MultiParentConstraint _bowMultiParent;
    [Header("1H Weapon")]
    [SerializeField] private Transform _oneHArmSlot;
    [SerializeField] private Transform _oneHBeltSlot;
    [SerializeField] private MultiParentConstraint _oneHMultiParent;
    [Header("2H Weapon")]
    [SerializeField] private Transform _twoHArmSlot;
    [SerializeField] private Transform _twoHBackSlot;
    [SerializeField] private MultiParentConstraint _twoHMultiParent;
    [Header("Shield")]
    [SerializeField] private Transform _shieldArmSlot;
    [SerializeField] private Transform _shieldBackSlot;
    [SerializeField] private MultiParentConstraint _shieldMultiParent;
    [Header("Quiver")]
    [SerializeField] private Transform _quiverSlot;
    [Header("Arrow")]
    [SerializeField] private Transform _arrowHandSlot;

    public MultiParentConstraint BowMultiParent => _bowMultiParent;
    public MultiParentConstraint OneHMultiParent => _oneHMultiParent;
    public MultiParentConstraint TwoHMultiParent => _twoHMultiParent;
    public MultiParentConstraint ShieldMultiParent => _shieldMultiParent;

    public List<Transform> GetBowSlots()
    {
        return new List<Transform>
        {
            _bowHandSlot, _bowBackSlot
        };
    }
    public List<Transform> Get1HWeaponSlots()
    {
        return new List<Transform>
        {
            _oneHArmSlot, _oneHBeltSlot
        };
    }
    public List<Transform> Get2HWeaponSlots()
    {
        return new List<Transform>
        {
            _twoHArmSlot, _twoHBackSlot
        };
    }
    public List<Transform> GetShieldSlots()
    {
        return new List<Transform>
        {
            _shieldArmSlot, _shieldBackSlot
        };
    }
    public List<Transform> GetQuiverSlots()
    {
        return new List<Transform>
        {
            _quiverSlot
        };
    }
    public List<Transform> GetArrowSlots()
    {
        return new List<Transform>
        {
            _arrowHandSlot
        };
    }
}
