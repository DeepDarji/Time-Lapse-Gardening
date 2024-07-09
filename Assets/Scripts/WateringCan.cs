using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public float wateringRange = 1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf) // Only water when watering can is active
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(mousePosition, wateringRange);
            foreach (Collider2D collider in hitColliders)
            {
                Plant plant = collider.GetComponent<Plant>();
                if (plant != null && plant.gameObject.activeSelf) // Ensure plant is active
                {
                    plant.WaterPlant();
                    Debug.Log("Plant watered at position: " + mousePosition); // Log for debugging
                }
            }
        }
    }
}