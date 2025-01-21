using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private Transform _attachPoint;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void Init(Transform attachPoint)
    {
        _attachPoint = attachPoint;
    }

    public void SetHealthSliderValue(float health, float delta, float maxHealth)
    {
        healthSlider.value = health / maxHealth;
    }

    private void Update()
    {
        Vector3 attachScreenPoint = mainCamera.WorldToScreenPoint(_attachPoint.position);
        transform.position = attachScreenPoint;
    }

    public void OnOwnerDead() => Destroy(gameObject);
}
