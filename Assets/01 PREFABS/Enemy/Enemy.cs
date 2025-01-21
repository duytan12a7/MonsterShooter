using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private Animator animator;

    private void Start()
    {
        LoadComponent();

        healthComponent.onHealthEmpty += StartDeath;
        healthComponent.onTakeDamage += TakenDamage;
    }

    #region Load Components

    private void LoadComponent()
    {
        LoadHealthComponent();
        LoadAnimator();
    }

    private void LoadHealthComponent()
    {
        if (healthComponent != null) return;

        healthComponent = GetComponent<HealthComponent>();
        Debug.Log(transform.name + " : LoadHealthComponent", gameObject);
    }

    private void LoadAnimator()
    {
        if (animator != null) return;

        animator = GetComponent<Animator>();
        Debug.Log(transform.name + " : LoadAnimator", gameObject);
    }

    #endregion

    private void StartDeath()
    {
        TriggerDeathAnimation();
    }

    private void TakenDamage(float health, float delta, float maxHealth)
    {
    }

    private void TriggerDeathAnimation() => animator.SetTrigger("Dead");

    public void OnDeathAnimationFinished() => Destroy(gameObject);

}
