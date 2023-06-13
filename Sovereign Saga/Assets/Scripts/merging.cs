/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingItem : MonoBehaviour
{
    // Stats of building, including cost and how much it is producing per second
    private int buildingLvl = 0;
    private bool upgradedBuilding = false;

    [SerializeField]
    private int buildingCost;
    private int buildingProductionRate;

    // TODO: For UI NOTES -
    // Chek if isPurchased is false. If so, give option to buy.
    // When buy button is clicked, just change isPurchased to true.
    private bool isPurchased = false;

    private bool incomeUpdated = false;

    [SerializeField]
    public PlayerController playerController;

    [SerializeField]
    private GameObject buildingPurchaseUI;

    [SerializeField]
    private GameObject upgradeBuildingPurchaseUI;

    [SerializeField]
    private Button purchaseYesButton;

    [SerializeField]
    private Button purchaseUpgradeYesButton;

    [SerializeField]
    private Button purchaseUpgradeNoButton;

    [SerializeField]
    private Button purchaseNoButton;

    [SerializeField]
    private TextMeshProUGUI text;

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

                
               // buildingCost += buildingCost;
               // buildingProductionRate += buildingProductionRate;

                //buildingLvl += 1;
            }
        }

        if (isPurchased && upgradedBuilding){
                // Subtract cost of building from player income
                //playerController.SetCurrentIncome(playerController.GetCurrentIncome() - buildingCost);

                PlayerController.incomeGenerationRate += buildingProductionRate;
                //incomeUpdated = true;
                upgradedBuilding = false;
                //buildingLvl += 1;
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.isPurchased == false)
            {
                buildingPurchaseUI.SetActive(true);
                purchaseYesButton.onClick.AddListener(() => {
                    this.isPurchased = true;
                    //upgradedBuilding = true;
                    buildingPurchaseUI.SetActive(false);
                });
                purchaseNoButton.onClick.AddListener(() => {
                    buildingPurchaseUI.SetActive(false);
                });
            }
            else if (this.isPurchased == true)
            {
                if (buildingLvl >= 3)
                {
                   // text.text = "Building is Maxed Out\n";
                }
                upgradeBuildingPurchaseUI.SetActive(true);
                purchaseUpgradeYesButton.onClick.AddListener(() => {
                    buildingLvl++;
                    upgradedBuilding = true;
                    upgradeBuildingPurchaseUI.SetActive(false);
                });
                purchaseUpgradeNoButton.onClick.AddListener(() => {
                    upgradeBuildingPurchaseUI.SetActive(false);
                });

            }
        }
    }
}*/