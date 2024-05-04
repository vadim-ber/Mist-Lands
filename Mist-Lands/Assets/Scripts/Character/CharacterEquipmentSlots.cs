using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipmentSlots : MonoBehaviour
{
    [Header("Bow")]
    [SerializeField] private Transform _bowHandSlot;
    [SerializeField] private Transform _quiverSlot;
    [SerializeField] private Transform _arrowHandSlot;
    [SerializeField] private Transform _bowBackSlot; 
    [Header("1H Weapon")]
    [SerializeField] private Transform _oneHArmSlot;
    [SerializeField] private Transform _oneHBeltSlot;
    [Header("2H Weapon")]
    [SerializeField] private Transform _twoHArmSlot;
    [SerializeField] private Transform _twoHBackSlot;
    [Header("Shield")]
    [SerializeField] private Transform _shieldArmSlot;
    [SerializeField] private Transform _shieldBackSlot;

    public List<Transform> GetBowSlots()
    {
        return new List<Transform>
        {
            _bowHandSlot, _quiverSlot, _arrowHandSlot, _bowBackSlot
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
}
