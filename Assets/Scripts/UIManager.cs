using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] plantPrefabs; // Array to hold different plant prefabs
    public GameObject wateringCan;
    public Button[] plantButtons; // Array to hold different plant buttons
    public Button waterButton;
    public Button timeLapseButton;
    public TimeLapse timeLapse;

    private GameObject selectedPlantPrefab;
    private Vector2[] plantPositions = new Vector2[20]; // Array to store potential positions for plants
    private int currentPlantIndex = 0; // Track current index for plant positions

    void Start()
    {
        for (int i = 0; i < plantButtons.Length; i++)
        {
            int index = i; // Local copy of i for the lambda expression
            plantButtons[i].onClick.AddListener(() => SelectPlant(index));
        }

        waterButton.onClick.AddListener(EnableWatering);
        timeLapseButton.onClick.AddListener(ToggleTimeLapse);

        // Initialize plant positions
        InitializePlantPositions();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null)
            {
                if (hitCollider.gameObject == waterButton.gameObject)
                {
                    EnableWatering();
                }
                else if (hitCollider.gameObject == timeLapseButton.gameObject)
                {
                    ToggleTimeLapse();
                }
                else if (selectedPlantPrefab != null && currentPlantIndex < plantPositions.Length)
                {
                    PlantSeed();
                }
            }
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

    void SelectPlant(int index)
    {
        selectedPlantPrefab = plantPrefabs[index];
        Debug.Log("Selected plant: " + selectedPlantPrefab.name);
    }

    void PlantSeed()
    {
        if (selectedPlantPrefab != null && currentPlantIndex < plantPositions.Length)
        {
            Vector2 spawnPosition = plantPositions[currentPlantIndex];
            Instantiate(selectedPlantPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
            currentPlantIndex++;
        }
        else
        {
            Debug.Log("No more positions available for planting.");
        }
    }

    void EnableWatering()
    {
        wateringCan.SetActive(!wateringCan.activeSelf);
    }

    void ToggleTimeLapse()
    {
        timeLapse.ToggleTimeLapse();
    }
}