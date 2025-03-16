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
        
        var renderer = GetComponent<MeshRenderer>();
        _material = new Material(renderer.sharedMaterial);
        renderer.material = _material;
        
        _currentHeight = transform.position.y - playerHeight;
    }

    private void Update()
    {
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
