using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingItem : MonoBehaviour
{
    // Stats of building, including cost and how much it is producing per second
    //private int buildingLvl = 0;

    [SerializeField]
    private int buildingCost;
    private int buildingProductionRate;

    // TODO: For UI NOTES -
    // Chek if isPurchased is false. If so, give option to buy.
    // When buy button is clicked, just change isPurchased to true.
    private bool isPurchased = true;

    private bool incomeUpdated = false;

    [SerializeField]
    public PlayerController playerController;

    void Start()
    {
        // Formula to calculate how much building produces in correlation to cost of building (Can change later)
        buildingProductionRate = buildingCost / 1000;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Produce passive income IF building is bought
        if (isPurchased && !incomeUpdated)
        {
            // If we have enough to buy
            if (playerController.GetCurrentIncome() >= buildingCost)
            {
                // Subtract cost of building from player income
                playerController.SetCurrentIncome(playerController.GetCurrentIncome() - buildingCost);

                PlayerController.incomeGenerationRate = PlayerController.incomeGenerationRate + buildingProductionRate;
                incomeUpdated = true;
            }
        }
    }

}
