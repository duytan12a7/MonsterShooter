using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private AimComponent aimComp;

    public override void Attack()
    {
        GameObject target = aimComp.GetAimTarget();
        Debug.Log($"aiming at {target}");
    }
}
