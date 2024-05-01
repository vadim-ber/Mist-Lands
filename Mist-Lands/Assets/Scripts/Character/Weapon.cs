using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Items/Create new weapon")]
public class Weapon : ScriptableObject
{
    public enum CombatMode { Meele, Ranged };
    public enum WeaponSlot { Bow, RightArm, LeftArm, TwoHanded };


    [SerializeField] private CombatMode _combatMode;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private RuntimeAnimatorController _animatorController;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _attackRange = 2.5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private int _attackPrice = 1;
    private GameObject _weapon;

    public float AttackRange => _attackRange;
    public CombatMode Combat => _combatMode;
    public RuntimeAnimatorController AnimatorController => _animatorController;
    public float Damage => _damage;
    public int AttackPrice => _attackPrice;

    public void Initialize(WeaponSlotsHandler weaponSlotsHandler)
    {
        Transform slot = null;
        switch (_weaponSlot)
        {
            case WeaponSlot.Bow:
                slot = weaponSlotsHandler.BowSlot.transform;
                break;
            case WeaponSlot.RightArm:
                slot = weaponSlotsHandler.RightArmSlot.transform;
                break;
            case WeaponSlot.LeftArm:
                slot = weaponSlotsHandler.LeftArmSlot.transform;
                break;
            case WeaponSlot.TwoHanded:
                slot = weaponSlotsHandler.TwoHandedSlot.transform;
                break;
        }
        _weapon = Instantiate(_prefab);        
        _weapon.transform.position = Vector3.zero;
        _weapon.transform.rotation = Quaternion.identity;
        _weapon.transform.SetParent(slot, false);
        Debug.Log(_weaponSlot + " " + _weapon);
    }

    public void Clear()
    {
        Destroy(_weapon);
    }
}
