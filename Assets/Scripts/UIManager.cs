using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] plantPrefabs; // Array of different plant prefabs
    public GameObject wateringCan;
    public Button[] plantButtons; // Array of buttons for different plants
    public Button waterButton;
    public Button timeLapseButton;
    public TimeLapse timeLapse;

    private float plantSpacing = 1f; // Minimum distance between plants

    private Vector3[] area1 = {
        new Vector3(-7, 4, 0),
        new Vector3(5, 4, 0)
    };

    private Vector3[] area2 = {
        new Vector3(-7, -1, 0),
        new Vector3(5, -1, 0)
    };

    void Start()
    {
        for (int i = 0; i < plantButtons.Length; i++)
        {
            int index = i; // Capture the index
            plantButtons[i].onClick.AddListener(() => PlantSeed(index));
        }

        waterButton.onClick.AddListener(EnableWatering);

        // Add a listener for the time lapse button directly in code
        timeLapseButton.onClick.AddListener(ToggleTimeLapse);
    }

    void PlantSeed(int index)
    {
        Vector3 plantPosition = GetRandomPosition(index);
        if (!IsPositionOccupied(plantPosition))
        {
            // Check if watering button is active
            bool isWateringActive = wateringCan.activeSelf;

            // Instantiate the plant prefab
            GameObject newPlant = Instantiate(plantPrefabs[index], plantPosition, Quaternion.identity);

            // Set the plant's active state based on the watering button
            newPlant.SetActive(isWateringActive);

            // Log to console for debugging (optional)
            Debug.Log($"Planted plant {index}. Watering active: {isWateringActive}");
        }
    }

    Vector3 GetRandomPosition(int index)
    {
        Vector3 start, end;

        if (index == 0) // For the first plant type
        {
            start = area1[0];
            end = area1[1];
        }
        else // For the second plant type
        {
            start = area2[0];
            end = area2[1];
        }

        float randomX = UnityEngine.Random.Range(start.x, end.x); // Specify UnityEngine.Random here
        return new Vector3(randomX, start.y, 0);
    }

    bool IsPositionOccupied(Vector3 position)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, plantSpacing);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.GetComponent<Plant>() != null)
            {
                return true; // Position is occupied by another plant
            }
        }
        return false; // Position is free
    }

    void EnableWatering()
    {
        wateringCan.SetActive(!wateringCan.activeSelf);
    }

    void ToggleTimeLapse()
    {
        // Toggle the time lapse feature
        timeLapse.ToggleTimeLapse();
    }
}