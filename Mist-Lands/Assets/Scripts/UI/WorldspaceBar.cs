using UnityEngine;
using UnityEngine.UI;

public class WorldspaceBar : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private Text _armorText;
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private Transform _healthBarPivot;
    [SerializeField] private bool _hideZeroHealthBar = true;

    private void Start()
    {
        _armorText.text = _unit.Armor.ArmorValue.ToString();
    }
    void Update()
    {
        _healthBarImage.fillAmount = _unit.Health.CurrentHealtValue / _unit.Health.MaxHealthValue;
        _healthBarPivot.LookAt(Camera.main.transform.position);
        if (_hideZeroHealthBar)
        {
            _healthBarPivot.gameObject.SetActive(_unit.Health.CurrentHealtValue > 0);
        }
    }
}
