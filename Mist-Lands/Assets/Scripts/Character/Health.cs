using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealthValue;
    [SerializeField] private Unit _unit;
    // blood vfx prefab
    private float _currentHealthValue;
    private bool _isFall;
    private bool _receivedDamage;
    private bool _blockPossible;
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
    public bool BlockPossible
    {
        get => _blockPossible;
        set => _blockPossible = value;
    }

    private void Start()
    {
        _currentHealthValue = _maxHealthValue;
    }

    public void TakeDamage(float fullDamage)
    {
        if (_isFall)
        {
            return;
        }        
        _currentHealthValue -= fullDamage;
        if (fullDamage > 0)
        {
            _receivedDamage = true;
        }
        if (fullDamage <= 0)
        {
            _blockPossible = true;
        }
        if (_currentHealthValue <= 0)
        {
            _isFall = true;
        }
    }

    public float CalcDamage(float weaponDamage, int armorValue)
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
