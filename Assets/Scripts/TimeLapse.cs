using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLapse : MonoBehaviour
{
    public float timeScale = 10f; // Adjust this to control the speed of time-lapse

    private bool isTimeLapseActive = false;
    private float originalTimeScale;

    void Start()
    {
        originalTimeScale = Time.timeScale; // Store the original time scale
    }

    void Update()
    {
        if (isTimeLapseActive)
        {
            Time.timeScale = timeScale; // Set the time scale to speed up time
        }
        else
        {
            Time.timeScale = originalTimeScale; // Restore original time scale
        }
    }

    public void ToggleTimeLapse()
    {
        isTimeLapseActive = !isTimeLapseActive;
        Debug.Log("Time-lapse " + (isTimeLapseActive ? "enabled" : "disabled"));
    }
}