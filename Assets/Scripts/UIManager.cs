using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*This script manages planting different plant types at available positions in the garden, toggling the watering feature,
and controlling the time-lapse functionality. It ensures that plants are placed only where available and adjusts
planting availability based on whether the watering can is active or not.*/


public class UIManager : MonoBehaviour
{
    public GameObject[] plantPrefabs; // Array to hold different plant prefabs
    public GameObject wateringCan;
    public Button waterButton;
    public Button timeLapseButton;
    public TimeLapse timeLapse;

    private Vector2[] plantPositions = new Vector2[20]; // Array to store potential positions for plants
    private int currentPlantIndex = 0; // Track current index for plant positions

    private bool canPlant = true; // Flag to control planting availability

    void Start()
    {
        // Attach methods to UI buttons
        waterButton.onClick.AddListener(EnableWatering); // Listen for water button clicks
        timeLapseButton.onClick.AddListener(ToggleTimeLapse); // Listen for time-lapse button clicks

        // Initialize plant positions when the game starts
        InitializePlantPositions();
    }

    void Update()
    {
        // Check if left mouse button is clicked and planting is allowed
        if (Input.GetMouseButtonDown(0) && canPlant)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlantSeed(mousePosition); // Plant seed at mouse position
        }
    }

    // Initialize potential plant positions in the garden
    void InitializePlantPositions()
    {
        int index = 0;
        for (float x = -7; x <= 5; x += 2)
        {
            plantPositions[index] = new Vector2(x, 4); // Top row positions
            index++;
        }
        for (float x = -7; x <= 5; x += 2)
        {
            plantPositions[index] = new Vector2(x, -1); // Bottom row positions
            index++;
        }
    }

    // Plant a seed at the closest available position to the given position
    void PlantSeed(Vector2 position)
    {
        int randomPlantIndex = UnityEngine.Random.Range(0, plantPrefabs.Length); // Choose a random plant prefab
        GameObject selectedPlantPrefab = plantPrefabs[randomPlantIndex];

        Vector2 spawnPosition = GetClosestAvailablePlantPosition(position); // Find closest available planting spot

        if (spawnPosition != Vector2.zero)
        {
            Instantiate(selectedPlantPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
            currentPlantIndex++; // Increment plant count
        }
        else
        {
            Debug.Log("No available position found for planting."); // Log if no spot is available
        }
    }

    // Find the closest available plant position to the given position
    Vector2 GetClosestAvailablePlantPosition(Vector2 position)
    {
        float minDistance = float.MaxValue;
        Vector2 closestPosition = Vector2.zero;

        foreach (Vector2 plantPosition in plantPositions)
        {
            bool positionOccupied = Physics2D.OverlapCircle(plantPosition, 0.5f); // Check if position is occupied by a collider

            if (!positionOccupied)
            {
                float distance = Vector2.Distance(position, plantPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPosition = plantPosition; // Update closest position if available
                }
            }
        }

        return closestPosition;
    }

    // Enable or disable the watering can and update planting availability
    void EnableWatering()
    {
        bool isWateringActive = !wateringCan.activeSelf; // Toggle watering can active state
        wateringCan.SetActive(isWateringActive);

        canPlant = !isWateringActive; // Update planting availability based on watering can state
    }

    // Toggle the time-lapse feature
    void ToggleTimeLapse()
    {
        timeLapse.ToggleTimeLapse(); // Call TimeLapse script to toggle time-lapse feature
    }
}