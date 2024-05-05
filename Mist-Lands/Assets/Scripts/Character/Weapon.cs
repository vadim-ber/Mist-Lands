using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Weapon
{
    private readonly WeaponData _weaponData;
    private readonly List<GameObject> _models;
    private List<Transform> _slots;
    private List<Transform> _additionalSlots;
    private MultiParentConstraint _parentConstraint;
    public Weapon(WeaponData weaponData, CharacterEquipmentSlots equipmentSlots)
    {
        _weaponData = weaponData;
        _models = new();
        FindSlots(weaponData.WSlot, equipmentSlots);
        InitializeModel(weaponData.Prefab, _slots[0]);

        for (int i = 0; i < weaponData.AdditionalPrefabs.Count; i++)
        {
            InitializeModel(weaponData.AdditionalPrefabs[i], _additionalSlots[0]);
        }

        _parentConstraint.data.constrainedObject = _models[0].transform;
        var sourceObjects = _parentConstraint.data.sourceObjects;
        sourceObjects.Insert(0, new WeightedTransform(_slots[0], 0f));
        sourceObjects.Insert(1, new WeightedTransform(_slots[1], 1f));        
        _parentConstraint.data.sourceObjects = sourceObjects;
    }
    public WeaponData WeaponData => _weaponData;

    public void Unsheath()
    {
        var source = _parentConstraint.data.sourceObjects;
        source.SetWeight(0, 1f);
        source.SetWeight(1, 0f);
        _parentConstraint.data.sourceObjects = source;
    }
    public void Sheath()
    {
        var source = _parentConstraint.data.sourceObjects;
        _parentConstraint.data.sourceObjects.SetWeight(0, 0f);
        _parentConstraint.data.sourceObjects.SetWeight(1, 1f);
        _parentConstraint.data.sourceObjects = source;
    }

    private void FindSlots(WeaponData.WeaponSlot weaponSlot,
        CharacterEquipmentSlots equipmentSlots)
    {        
        switch (weaponSlot)
        {
            case WeaponData.WeaponSlot.Bow:
                _slots = equipmentSlots.GetBowSlots();
                _additionalSlots = equipmentSlots.GetQuiverSlots();
                _parentConstraint = equipmentSlots.BowMultiParent;
                break;
            case WeaponData.WeaponSlot.RightArm:
                _slots = equipmentSlots.Get1HWeaponSlots();
                _parentConstraint = equipmentSlots.OneHMultiParent;
                break;
            case WeaponData.WeaponSlot.TwoHanded:
                _slots = equipmentSlots.Get2HWeaponSlots();
                _parentConstraint = equipmentSlots.TwoHMultiParent;
                break;
            case WeaponData.WeaponSlot.LeftArm:
                _slots = equipmentSlots.GetShieldSlots();
                _parentConstraint = equipmentSlots.ShieldMultiParent;
                break;            
        }
    }

    private void InitializeModel(GameObject prefab, Transform position)
    {
        var model = GameObject.Instantiate(prefab);
        model.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        model.transform.SetParent(position, false);
        _models.Add(model);
    }
}
