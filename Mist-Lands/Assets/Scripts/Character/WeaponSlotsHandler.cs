using UnityEngine;

public class WeaponSlotsHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bowSlot;
    [SerializeField] private GameObject _rightArmSlot;
    [SerializeField] private GameObject _lefttArmSlot;
    [SerializeField] private GameObject _twoHandedSlot;

    public GameObject BowSlot
    {
        get => _bowSlot;
    }
    public GameObject RightArmSlot
    {
        get => _rightArmSlot;
    }
    public GameObject LeftArmSlot
    {
        get => _lefttArmSlot;
    }
    public GameObject TwoHandedSlot
    {
        get => _twoHandedSlot;   
    }
}
