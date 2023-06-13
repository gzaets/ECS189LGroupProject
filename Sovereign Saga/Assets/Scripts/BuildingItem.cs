using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingItem : MonoBehaviour
{
    // Stats of building, including cost and how much it is producing per second
    private int buildingLvl = 0;
    private bool upgradedBuilding = false;
    private bool playerIncomeUpdated = false;

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
    //public TextMeshProUGUI buildingPurchaseText;

    [SerializeField]
    private GameObject upgradeBuildingPurchaseUI;

    [SerializeField]
    private Button purchaseYesButton;

    [SerializeField]
    private Button purchaseNoButton;

    [SerializeField]
    private Button purchaseUpgradeYesButton;

    [SerializeField]
    private Button purchaseUpgradeNoButton;

    [SerializeField]
    private AudioSource purchaseBuildingSoundEffect;

    void Start()
    {
        // Formula to calculate how much building produces in correlation to cost of building (Can change later)
        buildingProductionRate = buildingCost / 1000;
        buildingCost = buildingCost;

        TextMeshProUGUI buildingPurchaseText = buildingPurchaseUI.GetComponentInChildren<TextMeshProUGUI>();
        buildingPurchaseText.text = "Do you want to purchase this building for $" + buildingCost;
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
        if(isPurchased && !playerIncomeUpdated)
        {
            playerController.money = playerController.money - buildingCost;
            playerIncomeUpdated = true;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //buildingPurchaseUI = Object.Instantiate(buildingPurchaseUI, buildingPurchaseUI.transform.position, Quaternion.identity);
        TextMeshProUGUI buildingPurchaseText = buildingPurchaseUI.GetComponentInChildren<TextMeshProUGUI>();
        buildingPurchaseText.text = "Do you want to purchase this building for $" + buildingCost;

        purchaseNoButton.onClick.RemoveAllListeners();
        purchaseYesButton.onClick.RemoveAllListeners();
        //Debug.Log(gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            if(playerController.money - buildingCost >= 0) {
            if (this.isPurchased == false)
            {
                buildingPurchaseUI.SetActive(true);
                purchaseYesButton.onClick.AddListener(() => {
                    this.isPurchased = true;
                    //Debug.Log("just pruchased\n");
                    playerController.numBuildingsPurchased++;
                    StartCoroutine(playSound());
                    //purchaseBuildingSoundEffect.Play();
                    buildingPurchaseUI.SetActive(false);
                });
                purchaseNoButton.onClick.AddListener(() => {
                    buildingPurchaseUI.SetActive(false);
                });
            }
            else if (this.isPurchased == true)
            {

                upgradeBuildingPurchaseUI.SetActive(true);
                purchaseUpgradeYesButton.onClick.AddListener(() => {
                    buildingLvl++;
                    upgradedBuilding = true;
                    StartCoroutine(playSound());
                    upgradeBuildingPurchaseUI.SetActive(false);
                });
                purchaseUpgradeNoButton.onClick.AddListener(() => {
                    upgradeBuildingPurchaseUI.SetActive(false);
                });
            }
            }
        }
    }

    IEnumerator playSound()
     {
        purchaseBuildingSoundEffect.Play();
        yield return new WaitForSeconds(purchaseBuildingSoundEffect.clip.length);
        purchaseBuildingSoundEffect.Play();
     }
}