using UnityEngine;

[CreateAssetMenu(fileName = "ArmorData", menuName = "ScriptableObjects/Items/Create new armor")]
public class Armor : ScriptableObject
{
    [SerializeField] private int _armorValue;

    public int Value
    {
        get => _armorValue;
    }
}
