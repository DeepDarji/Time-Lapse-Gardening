using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



/*This script allows the player to water plants within a specified range using a watering can.
It checks for mouse clicks and ensures the watering can is active before attempting to water any plants.
If a plant is found within the defined range, it calls the WaterPlant() method on the Plant component attached to that plant, 
simulating the watering process. Debug logs are used to provide feedback on which plant was watered and at what position.*/



public class WateringCan : MonoBehaviour
{
    public float wateringRange = 1f; // Range within which the watering can can affect plants

    void Update()
    {
        // Check if left mouse button is pressed and the watering can is active
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convert mouse position to world coordinates
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(mousePosition, wateringRange); // Find all colliders within the watering range
            foreach (Collider2D collider in hitColliders)
            {
                Plant plant = collider.GetComponent<Plant>(); // Get the Plant component from the collider
                if (plant != null && plant.gameObject.activeSelf) // Check if a valid plant is found and it's active
                {
                    plant.WaterPlant(); // Water the plant
                    Debug.Log("Plant watered at position: " + mousePosition); // Log watering action for debugging
                }
            }
        }
    }
}