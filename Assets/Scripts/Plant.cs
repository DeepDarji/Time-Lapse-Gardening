using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



/*This script controls the growth of a plant through different stages based on watering. 
It updates the plant's sprite to visually represent its growth and ensures it remains above other elements on the screen. 
The WaterPlant() method is used to simulate watering the plant, triggering its growth process.*/



public class Plant : MonoBehaviour
{
    public Sprite[] growthStages; // Array of sprites representing growth stages
    public float timeToGrow = 10f; // Time it takes for the plant to grow to the next stage

    private int currentStage = 0; // Current stage of plant growth
    private float timer = 0f; // Timer to track time since last watering
    private bool isWatered = false; // Flag indicating if the plant has been watered

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = growthStages[currentStage]; // Set initial sprite at start
        GetComponent<SpriteRenderer>().sortingLayerName = "Default"; // Set sorting layer for sprite
        GetComponent<SpriteRenderer>().sortingOrder = 1; // Ensure plant is above other elements on screen
    }

    void Update()
    {
        // Check if the plant is watered and can grow
        if (isWatered)
        {
            timer += Time.deltaTime; // Increment timer based on real-time
            Debug.Log("Timer: " + timer); // Log timer for debugging purposes

            // If enough time has passed and there are more growth stages
            if (timer > timeToGrow && currentStage < growthStages.Length - 1)
            {
                currentStage++; // Move to the next growth stage
                GetComponent<SpriteRenderer>().sprite = growthStages[currentStage]; // Update plant sprite
                timer = 0f; // Reset timer
                isWatered = false; // Reset watered state for next growth cycle
                Debug.Log("Plant grew to stage: " + currentStage); // Log growth stage for debugging
            }
        }
    }

    // Method to water the plant, triggering growth
    public void WaterPlant()
    {
        isWatered = true; // Set plant as watered
        Debug.Log("Plant has been watered."); // Log watering action for debugging
    }
}
