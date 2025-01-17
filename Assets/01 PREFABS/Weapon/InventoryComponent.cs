using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private Weapon[] initWeaponPrefabs;

    [SerializeField] private Transform defaultWeaponSlot;
    [SerializeField] private Transform[] weaponSlots;

    private List<Weapon> weapons;
    private int currentWeaponIndex = -1;

    private void Start()
    {
        InitializeWeapon();
    }

    private void InitializeWeapon()
    {
        weapons = new List<Weapon>();
        foreach (Weapon weapon in initWeaponPrefabs)
        {
            Transform weaponSlot = defaultWeaponSlot;
            foreach (Transform slot in weaponSlots)
            {
                if (slot.gameObject.tag == weapon.GetAttachSlotTag())
                    weaponSlot = slot;
            }
            Weapon newWeapon = Instantiate(weapon, weaponSlot);
            newWeapon.Init(gameObject);
            weapons.Add(newWeapon);
        }

        NextWeapon();
    }

    public void NextWeapon()
    {
        int nextWeaponIndex = currentWeaponIndex + 1;

        if (nextWeaponIndex >= weapons.Count)
            nextWeaponIndex = 0;

        EquipWeapon(nextWeaponIndex);
    }

    private void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
            return;

        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
            weapons[currentWeaponIndex].UnEquip();

        weapons[weaponIndex].Equip();
        currentWeaponIndex = weaponIndex;
    }

    public Weapon GetActiveWeapon() => weapons[currentWeaponIndex];
}
