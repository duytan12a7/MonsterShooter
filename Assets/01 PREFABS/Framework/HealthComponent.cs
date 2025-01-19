using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    #region Events and Delegates

    public delegate void OnHealthChange(float health, float delta, float maxHealth);
    public delegate void OnTakeDamage(float health, float delta, float maxHealth);
    public delegate void OnHealthEmpty();
    public event OnTakeDamage onTakeDamage;
    public event OnHealthChange onHealthChange;
    public event OnHealthEmpty onHealthEmpty;

    #endregion

    [Header("Settings")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float maxHealth = 100f;

    public void ChangeHealth(float amount)
    {
        if (amount == 0) return;

        health = Mathf.Clamp(health - amount, 0, maxHealth);

        onTakeDamage?.Invoke(health, amount, maxHealth);
        onHealthChange?.Invoke(health, amount, maxHealth);

        if (health == 0)
        {
            onHealthEmpty?.Invoke();
        }

        Debug.Log($"{gameObject.name}, taking damage: -{amount}, health is now: {health}");
    }
}
