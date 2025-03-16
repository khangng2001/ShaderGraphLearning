using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeFrac : MonoBehaviour
{
   [SerializeField] private TimeEnum timeEnum;
    [SerializeField] private TextMeshPro _textTimer;
   
    private TimeController _timeController;
    private int _timeValue;
    private int _currentValue;
    private Material _countDownMat;
    private float _normalizedValue;

    private void Awake()
    {
        _textTimer = GetComponentInChildren<TextMeshPro>();
        _timeController = GetComponentInParent<TimeController>();
        _countDownMat = GetComponent<MeshRenderer>().material;
    }
    
    private void Update()
    {
        // Calculate the normalized progress value for the shader
        CalculateProgress();
        
        // Update the text if the value has changed
        if (_timeValue != _currentValue)
        {
            _timeValue = _currentValue;
            _textTimer.text = _timeValue.ToString();
        }
        
        // Update the material with the progress value
        _countDownMat.SetFloat("_Progress", _normalizedValue);
    }
    
    private void CalculateProgress()
    {
        switch (timeEnum)
        {
            case TimeEnum.Days:
                // Display the current day
                _currentValue = _timeController.RemainingTime.Days;
                
                // Calculate the progress value for the shader
                // We want this value to decrease as time passes for counter-clockwise motion
                if (_timeController.RequiredTime.TotalDays > 0)
                {
                    _normalizedValue = (float)(_timeController.RemainingTime.TotalDays / _timeController.RequiredTime.TotalDays);
                }
                else
                {
                    _normalizedValue = 0; // No days required
                }
                break;
                
            case TimeEnum.Hours:
                // Display the current hour
                _currentValue = _timeController.RemainingTime.Hours;
                
                // Calculate the progress value for the shader
                // We want this value to decrease as time passes for counter-clockwise motion
                
                if (_timeController.RequiredTime.TotalHours <= 0)
                {
                    _normalizedValue = 0; 
                }
                // Special case for when required days is 0, but we have hours
                else if (_timeController.RequiredTime.Days == 0)
                {
                    // Normalize based on total required hours instead of 24-hour cycle
                    float totalRequiredHours = (float)_timeController.RequiredTime.TotalHours;
                    float remainingHours = (float)_timeController.RemainingTime.TotalHours;
                    
                    _normalizedValue = remainingHours / totalRequiredHours;
                }
                else
                {
                    // Normal case: Use a 24-hour cycle
                    float hourValue = _currentValue;
                    float minuteFraction = _timeController.RemainingTime.Minutes / 60.0f;
                    
                    _normalizedValue = (hourValue + minuteFraction) / 24.0f;
                }
                break;
                
            case TimeEnum.Minute:
              
                _currentValue = _timeController.RemainingTime.Minutes;
                
                if (_timeController.RequiredTime.TotalMinutes <= 0)
                {
                    _normalizedValue = 0; 
                }
                // Special case for when required hours is 0 but we have minutes
                else if (_timeController.RequiredTime.Hours == 0 && _timeController.RequiredTime.Days == 0)
                {
                    // Normalize based on total required minutes instead of 60-minute cycle
                    float totalRequiredMinutes = (float)_timeController.RequiredTime.TotalMinutes;
                    float remainingMinutes = (float)_timeController.RemainingTime.TotalMinutes;
                    
                    _normalizedValue = remainingMinutes / totalRequiredMinutes;
                }
                else
                {
                    // Normal case: Use a 60-minute cycle
                    float minuteValue = _currentValue;
                    float secondFraction = _timeController.RemainingTime.Seconds / 60.0f;
                    
                    _normalizedValue = (minuteValue + secondFraction) / 60.0f;
                }
                break;
                
            case TimeEnum.Second:
                _currentValue = _timeController.RemainingTime.Seconds;
                
                if (_timeController.RequiredTime.TotalSeconds <= 0)
                {
                    _normalizedValue = 0; 
                }
                // Special case for when required minutes is 0, but we have seconds
                else if (_timeController.RequiredTime.Minutes == 0 && 
                         _timeController.RequiredTime.Hours == 0 && 
                         _timeController.RequiredTime.Days == 0)
                {
                    // Normalize based on total required seconds instead of 60-second cycle
                    float totalRequiredSeconds = (float)_timeController.RequiredTime.TotalSeconds;
                    float remainingSeconds = (float)_timeController.RemainingTime.TotalSeconds;
                    
                    _normalizedValue = remainingSeconds / totalRequiredSeconds;
                }
                else
                {
                    // Normal case: Use a 60-second cycle
                    float secondValue = _currentValue;
                    float millisecondFraction = _timeController.RemainingTime.Milliseconds / 1000.0f; //
                    
                    _normalizedValue = (secondValue + millisecondFraction) / 60.0f;
                }
                break;
        }
        
        // Ensure the value is in valid range
        _normalizedValue = Mathf.Clamp01(_normalizedValue);
    }
   
}
