using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealthValue;
    [SerializeField] private Unit _unit;
    // blood vfx prefab
    private float _currentHealthValue;
    private bool _isFall;
    private bool _receivedDamage;
    public bool IsFall
    {
        get => _isFall;
    }
    public bool ReceivedDamage
    {
        get => _receivedDamage;  
        set => _receivedDamage = value;
    }
    public float MaxHealthValue
    {
        get => _maxHealthValue;
    }
    public float CurrentHealthValue
    {
        get => _currentHealthValue;
    }

    private void Start()
    {
        _currentHealthValue = _maxHealthValue;
    }

    public void TakeDamage(float damage)
    {
        if (_isFall)
        {
            return;
        }
        float fullDamage = CalcDamage(damage, _unit.Armor.Value);
        _currentHealthValue -= fullDamage;
        if (fullDamage > 0)
        {
            _receivedDamage = true;
        }
        if (_currentHealthValue <= 0)
        {
            _isFall = true;
            print("убит");
        }
    }

    private float CalcDamage(float weaponDamage, int armorValue)
    {
        if (weaponDamage >= armorValue * 2)
        {
            return weaponDamage;
        }
        else
        {
            return weaponDamage - armorValue;
        }
    }
}
