using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string attackSlotTag;
    public string GetAttachSlotTag() => attackSlotTag;

    [SerializeField] private AnimatorOverrideController animatorOverride;

    public GameObject Owner { get; private set; }

    public void Init(GameObject owner)
    {
        Owner = owner;
        UnEquip();
    }

    public void Equip()
    {
        gameObject.SetActive(true);
        Owner.GetComponent<Animator>().runtimeAnimatorController = animatorOverride;
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }
}
