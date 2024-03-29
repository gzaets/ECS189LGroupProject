using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingItem : MonoBehaviour
{
    // Stats of building, including cost and how much it is producing per second
    private int buildingLvl = 10;
    private bool upgradedBuilding = true;
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
    private GameObject buildingCantAffordUI;

    [SerializeField]
    private GameObject buildingAPUI;

    [SerializeField]
    private Button purchaseYesButton;

    [SerializeField]
    private Button purchaseNoButton;

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private Button closeAPButton;

    [SerializeField]
    private AudioClip purchaseBuildingSoundEffect;

    private AudioSource audioMusicSource;

    void Start()
    {
        audioMusicSource = gameObject.AddComponent<AudioSource>();
        audioMusicSource.volume = 1f;
        // Formula to calculate how much building produces in correlation to cost of building (Can change later)
        buildingProductionRate = buildingCost / 2000;
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

                PlayerController.incomeGenerationRate = PlayerController.incomeGenerationRate + (buildingProductionRate * buildingLvl);
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
            if(playerController.money - buildingCost >= 0 && this.isPurchased == false) {
                    buildingPurchaseUI.SetActive(true);
                    purchaseYesButton.onClick.AddListener(() => {
                        this.isPurchased = true;
                        playerController.numBuildingsPurchased++;
                        buildingPurchaseUI.SetActive(false);
                        audioMusicSource.clip = purchaseBuildingSoundEffect;
                        audioMusicSource.Play();
                    });
                    purchaseNoButton.onClick.AddListener(() => {
                        buildingPurchaseUI.SetActive(false);
                    });
            }
            else if (playerController.money - buildingCost < 0 && this.isPurchased == false){
                //cant afford ui
                    buildingCantAffordUI.SetActive(true);
                    closeButton.onClick.AddListener(() => {
                        buildingCantAffordUI.SetActive(false);
                    });
                    closeButton.onClick.AddListener(() => {
                        buildingCantAffordUI.SetActive(false);
                    });
            }
            else {
                Debug.Log("we in here");
                buildingAPUI.SetActive(true);
                closeAPButton.onClick.AddListener(() => {
                    buildingAPUI.SetActive(false);
                });
                closeAPButton.onClick.AddListener(() => {
                    buildingAPUI.SetActive(false);
                });
            }
        }
    }
}