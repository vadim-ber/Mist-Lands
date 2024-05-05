using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Items/Create new weapon")]
public class WeaponData : ScriptableObject
{
    public enum CombatMode { Meele, Ranged };
    public enum WeaponSlot { Bow, RightArm, LeftArm, TwoHanded };

    [SerializeField] private GameObject _prefab;
    [SerializeField] private List<GameObject> _additionalPrefabs;
    [SerializeField] private CombatMode _combatMode;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private RuntimeAnimatorController _animatorController;    
    [SerializeField] private float _attackRange = 2.5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private int _attackPrice = 1;

    public float AttackRange => _attackRange;
    public CombatMode Combat => _combatMode;
    public WeaponSlot WSlot => _weaponSlot;
    public RuntimeAnimatorController AnimatorController => _animatorController;
    public GameObject Prefab => _prefab;    
    public List<GameObject> AdditionalPrefabs => _additionalPrefabs;
    public float Damage => _damage;
    public int AttackPrice => _attackPrice;
}
