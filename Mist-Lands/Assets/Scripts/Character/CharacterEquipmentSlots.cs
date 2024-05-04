using UnityEngine;

public class CharacterEquipmentSlots : MonoBehaviour
{
    [Header("Bow")]
    [SerializeField] private Transform _bowHandSlot;
    [SerializeField] private Transform _bowBackSlot;
    [SerializeField] private Transform _quiverSlot;
    [SerializeField] private Transform _arrowHandSlot;
    [Header("1H Weapon")]
    [SerializeField] private Transform _oneHArmSlot;
    [SerializeField] private Transform _oneHBeltSlot;
    [Header("2H Weapon")]
    [SerializeField] private Transform _twoHArmSlot;
    [SerializeField] private Transform _twoHBackSlot;
    [Header("Shield")]
    [SerializeField] private Transform _shieldArmSlot;
    [SerializeField] private Transform _shieldBackSlot;

    public Transform BowHandSlot
    {
        get => _bowHandSlot;
    }
    public Transform BowBackSlot
    {
        get => _bowBackSlot;
    }
    public Transform QuiverSlot
    {
        get => _quiverSlot;
    }
    public Transform ArrowInHandSLot
    {
        get => _arrowHandSlot;
    }
    public Transform OneHArmSlot
    {
        get => _oneHArmSlot;
    }
    public Transform OneHBeltSlot
    {
        get => _oneHBeltSlot;
    }
    public Transform TwoHArmSlot
    {
        get => _twoHArmSlot;
    }
    public Transform TwoHBackSlot
    {
        get => _twoHBackSlot;
    }
    public Transform ShieldArmSlot
    {
        get => _shieldArmSlot;
    }
    public Transform ShieldBackSlot
    {
        get => _shieldBackSlot;
    }
}
