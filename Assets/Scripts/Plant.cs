using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plant : MonoBehaviour
{
    public Sprite[] growthStages;
    public float timeToGrow = 10f;

    private int currentStage = 0;
    private float timer = 0f;
    private bool isWatered = false;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = growthStages[currentStage];

        // Ensure the plant is on the correct sorting layer
        GetComponent<SpriteRenderer>().sortingLayerName = "Default"; // or your desired layer
        GetComponent<SpriteRenderer>().sortingOrder = 1; // Ensure it's above the canvas
    }

    void Update()
    {
        // Plant should only grow if watered
        if (isWatered)
        {
            timer += Time.deltaTime;
            Debug.Log("Timer: " + timer);

            if (timer > timeToGrow && currentStage < growthStages.Length - 1)
            {
                currentStage++;
                GetComponent<SpriteRenderer>().sprite = growthStages[currentStage];
                timer = 0f;
                isWatered = false; // Reset isWatered after growing
                Debug.Log("Plant grew to stage: " + currentStage);
            }
        }
    }

    public void WaterPlant()
    {
        isWatered = true;
        Debug.Log("Plant has been watered.");
    }
}