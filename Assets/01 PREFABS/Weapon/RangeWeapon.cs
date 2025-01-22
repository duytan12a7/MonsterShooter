using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private AimComponent aimComp;
    [SerializeField] private float damage = 5f;
    [SerializeField] private ParticleSystem bulletVFX;

    public override void Attack()
    {
        GameObject target = aimComp.GetAimTarget(out Vector3 aimDir);
        DamageGameObject(target, damage);

        bulletVFX.transform.rotation = Quaternion.LookRotation(aimDir);
        bulletVFX.Emit(bulletVFX.emission.GetBurst(0).maxCount);
    }
}
