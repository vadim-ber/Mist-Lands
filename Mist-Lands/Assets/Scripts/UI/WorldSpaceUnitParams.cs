using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceUnitParams : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private Text _attackText;
    [SerializeField] private Text _defenceText;
    [SerializeField] private Transform _paramsBarPivot;

    void Update()
    {
        _attackText.text = (Mathf.Round(_unit.CurrentAttackValue * 10f) / 10f).ToString();
        _defenceText.text = (Mathf.Round(_unit.CurrentDefenceValue * 10f) / 10f).ToString();
        _paramsBarPivot.LookAt(Camera.main.transform.position);
    }
}
