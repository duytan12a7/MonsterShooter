using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIComponent : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private HealthBar healthBarToSpawn;
    [SerializeField] private Transform healthBarAttachPoint;
    [SerializeField] private HealthComponent healthComponent;
    private InGameUI inGameUI;

    private void Start()
    {
        LoadHealthComponent();
        inGameUI = FindObjectOfType<InGameUI>();
        InitHealthBar();
    }

    private void LoadHealthComponent()
    {
        if (healthComponent != null) return;

        healthComponent = GetComponent<HealthComponent>();
        Debug.Log(transform.name + " : LoadHealthComponent", gameObject);
    }

    private void InitHealthBar()
    {
        HealthBar newHealthBar = Instantiate(healthBarToSpawn, inGameUI.transform);
        newHealthBar.Init(healthBarAttachPoint);
        healthComponent.onHealthChange += newHealthBar.SetHealthSliderValue;
        healthComponent.onHealthEmpty += newHealthBar.OnOwnerDead;
    }
}
