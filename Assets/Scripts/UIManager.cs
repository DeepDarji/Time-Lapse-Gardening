using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        waterButton.onClick.AddListener(EnableWatering);
        timeLapseButton.onClick.AddListener(ToggleTimeLapse);

        // Initialize plant positions
        InitializePlantPositions();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canPlant) // Check if planting is allowed
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlantSeed(mousePosition);
        }
    }

    void InitializePlantPositions()
    {
        int index = 0;
        for (float x = -7; x <= 5; x += 2) // Adjust step as needed
        {
            plantPositions[index] = new Vector2(x, 4);
            index++;
        }
        for (float x = -7; x <= 5; x += 2) // Adjust step as needed
        {
            plantPositions[index] = new Vector2(x, -1);
            index++;
        }
    }

    void PlantSeed(Vector2 position)
    {
        int randomPlantIndex = UnityEngine.Random.Range(0, plantPrefabs.Length); // Use UnityEngine.Random
        GameObject selectedPlantPrefab = plantPrefabs[randomPlantIndex];

        // Find closest available position
        Vector2 spawnPosition = GetClosestAvailablePlantPosition(position);

        if (spawnPosition != Vector2.zero)
        {
            Instantiate(selectedPlantPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
            currentPlantIndex++;
        }
        else
        {
            Debug.Log("No available position found for planting.");
        }
    }

    Vector2 GetClosestAvailablePlantPosition(Vector2 position)
    {
        // Find the closest available plant position to the clicked position
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
                    closestPosition = plantPosition;
                }
            }
        }

        return closestPosition;
    }

    void EnableWatering()
    {
        bool isWateringActive = !wateringCan.activeSelf;
        wateringCan.SetActive(isWateringActive);

        // Update planting availability based on watering can state
        canPlant = !isWateringActive;
    }

    void ToggleTimeLapse()
    {
        timeLapse.ToggleTimeLapse();
    }
}