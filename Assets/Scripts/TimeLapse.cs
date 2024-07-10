using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



/*This script allows you to control the speed of time in the game. 
When the ToggleTimeLapse() method is called, it toggles the isTimeLapseActive flag,
which either speeds up time by setting Time.timeScale to timeScale or restores the original time scale when time-lapse is disabled. 
Debug logs are used to indicate whether time-lapse is enabled or disabled for debugging purposes. 
Adjust timeScale to control how fast time progresses during time-lapse mode.*/



public class TimeLapse : MonoBehaviour
{
    public float timeScale = 10f; // Controls the speed of time-lapse
    private bool isTimeLapseActive = false; // Flag to indicate if time-lapse is active
    private float originalTimeScale; // Stores the original time scale

    void Start()
    {
        originalTimeScale = Time.timeScale; // Store the original time scale at the start
    }

    void Update()
    {
        // Check if time-lapse is active
        if (isTimeLapseActive)
        {
            Time.timeScale = timeScale; // Set the time scale to speed up time
        }
        else
        {
            Time.timeScale = originalTimeScale; // Restore original time scale
        }
    }

    // Method to toggle the time-lapse feature on and off
    public void ToggleTimeLapse()
    {
        isTimeLapseActive = !isTimeLapseActive; // Toggle the time-lapse state
        Debug.Log("Time-lapse " + (isTimeLapseActive ? "enabled" : "disabled")); // Log the state change for debugging
    }
}