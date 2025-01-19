using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private AimComponent aimComp;
    [SerializeField] private float damage = 5f;

    public override void Attack()
    {
        GameObject target = aimComp.GetAimTarget();
        DamageGameObject(target, damage);
    }
}
