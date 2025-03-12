using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.5f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float dissolveSpeed = 1f;

    private Material _material;
    private float _currentHeight;

    private void Awake()
    {
        // Create a material instance to avoid modifying the shared material
        var renderer = GetComponent<MeshRenderer>();
        _material = new Material(renderer.sharedMaterial);
        renderer.material = _material;
    
        // Initialize the starting height
        _currentHeight = transform.position.y - playerHeight;
    }

    private void Update()
    {
        // Smoothly update the height
        _currentHeight += Time.deltaTime * dissolveSpeed;
        SetEffect(_currentHeight);
    }

    private void SetEffect(float height)
    {
        if (_material != null)
        {
            _material.SetFloat("_CutOffHeight", height);
            _material.SetFloat("_NoiseStrength", noiseStrength);
        }
    }
}
