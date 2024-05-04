using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private readonly WeaponData _weaponData;
    private readonly List<GameObject> _models;    
    public Weapon(WeaponData weaponData, CharacterEquipmentSlots equipmentSlots)
    {
        _weaponData = weaponData;
        _models = new();
        var slots = FindSlots(weaponData.WSlot, equipmentSlots);
        for(int i = 0; i < _weaponData.Prefabs.Count; i++)
        {
            _models.Add(GameObject.Instantiate(weaponData.Prefabs[i]));
            _models[i].transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            _models[i].transform.SetParent(slots[i], false);
        }
    }
    public WeaponData WeaponData => _weaponData;

    private List<Transform> FindSlots(WeaponData.WeaponSlot weaponSlot,
        CharacterEquipmentSlots equipmentSlots)
    {
        List<Transform> slots = new();
        switch (weaponSlot)
        {
            case WeaponData.WeaponSlot.Bow:
                slots = equipmentSlots.GetBowSlots();
                break;
            case WeaponData.WeaponSlot.RightArm:
                slots = equipmentSlots.Get1HWeaponSlots();
                break;
            case WeaponData.WeaponSlot.LeftArm:
                slots = equipmentSlots.GetShieldSlots();
                break;
            case WeaponData.WeaponSlot.TwoHanded:
                slots = equipmentSlots.Get2HWeaponSlots();
                break;
        }
        return slots;
    }
}
