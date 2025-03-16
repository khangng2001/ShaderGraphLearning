using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private int requiredDay;
    [SerializeField] private int requiredHours;
    [SerializeField] private int requiredMinutes;
    [SerializeField] private int requiredSecond;

    private DateTime _targetTime;
    public TimeSpan RemainingTime;
    public TimeSpan RequiredTime { get; private set; } // Add this property

    private void Start()
    {
        AddRequirementTime(requiredDay, requiredHours, requiredMinutes, requiredSecond);
    }

    private void AddRequirementTime(int days, int hours, int minutes, int seconds)
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan requirementTime = new TimeSpan(days, hours, minutes, seconds);
        _targetTime = currentTime.Add(requirementTime);
        RequiredTime = requirementTime; // Store the required time
    }

    private void Update()
    {
        TimeRemaining();
    }

    private void TimeRemaining()
    {
        RemainingTime = _targetTime - DateTime.Now;
    }
}

public enum TimeEnum
{
    Days,
    Hours,
    Minute,
    Second
}
