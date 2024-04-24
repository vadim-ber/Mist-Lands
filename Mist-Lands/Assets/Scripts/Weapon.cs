using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapon/Create new weapon")]
public class Weapon : ScriptableObject
{
    public enum CombatMode
    {
        Meele,
        Ranged
    };

    [SerializeField] private CombatMode _combatMode;
    [SerializeField] private RuntimeAnimatorController _animatorController;
    [SerializeField] private float _attackRange = 2.5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private int _attackPrice = 1;

    public float AttackRange
    {
        get => _attackRange;
    }
    public CombatMode Combat
    {
        get => _combatMode;
    }
    public RuntimeAnimatorController AnimatorController
    {
        get => _animatorController;
    }
    public float Damage
    {
        get => _damage;
    }
    public int AttackPrice
    {
        get => _attackPrice;
    }
}
