using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealthValue;
    private float _currentHealtValue;
    private bool _isFall;
    public UnityAction OnFall;
    public UnityAction OnTakeDamage;
    public bool IsFall
    {
        get => _isFall;
    }
    public float MaxHealthValue
    {
        get => _maxHealthValue;
    }
    public float CurrentHealtValue
    {
        get => _currentHealtValue;
    }

    private void Start()
    {
        _currentHealtValue = _maxHealthValue;
    }

    public void TakeDamage(float damage)
    {
        if (_isFall)
        {
            return;
        }

        _currentHealtValue -= damage;
        OnTakeDamage?.Invoke();
        if (_currentHealtValue <= 0)
        {
            _isFall = true;
            OnFall?.Invoke();
            print("убит");
        }
    }
}
