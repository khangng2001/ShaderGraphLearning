using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float damgeValue = 2f;

    private float _currentHealth;
     private Material _materialHealthBar;
     private Quaternion _initialHealthBarRotation;
     
    private void Awake()
    {
        _currentHealth = maxHealth;
        _materialHealthBar = healthBar.GetComponent<MeshRenderer>().sharedMaterial;
        _initialHealthBarRotation = healthBar.transform.rotation;
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }
        UpdateHealth(_currentHealth);
        BeingShot();
        healthBar.transform.rotation = _initialHealthBarRotation;
    }

    private void UpdateHealth(float health)
    {
        float normalizeHealth = health / maxHealth;
        _materialHealthBar.SetFloat("_HealthAmount", normalizeHealth);
    }

    private void BeingShot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentHealth -= damgeValue;
        }
    }
    
}
