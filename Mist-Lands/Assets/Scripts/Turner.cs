using UnityEngine;
using UnityEngine.UI;

public class Turner : MonoBehaviour
{
    [SerializeField] private Unit[] _units;
    [SerializeField] Text _text;
    private int _count = 0;

    public void TurnEnd()
    {
        foreach (var unit in _units)
        {
            unit.NewTurn();
        }
        _count++;
        _text.text = _count.ToString();
    }    
}
