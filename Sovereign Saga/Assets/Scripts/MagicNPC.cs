using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MagicNPC : MonoBehaviour
{
    [SerializeField]
    public PlayerController playerController;

    [SerializeField]
    private GameObject magicPurchaseUI;

    [SerializeField]
    private Button purchaseYesButton;

    [SerializeField]
    private Button purchaseNoButton;

    private string type;

    private bool isPurchased = false;
    private bool playerIncomeUpdated = false;

    void Start()
    {
        // Formula to calculate how much building produces in correlation to cost of building (Can change later)
        TextMeshProUGUI magicPurchaseText = magicPurchaseUI.GetComponentInChildren<TextMeshProUGUI>();
        magicPurchaseText.text = "Do you want to purchase this magic for $100?";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.name == "Suck_Prefab") type = "Suck";
        else if(gameObject.name == "Tornado_Prefab") type = "Tornado";
        else if(gameObject.name == "Fireball_Prefab") type = "Fireball";
        else if(gameObject.name == "Rock_Prefab") type = "Rock";
        else ;
        //Debug.Log(gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            if (this.isPurchased == false)
            {
                magicPurchaseUI.SetActive(true);
                purchaseYesButton.onClick.AddListener(() => {
                    // Needs to check if requirements met, if so then:
                    playerController.BoughtMagic(type);
                    this.isPurchased = true;
                    // Needs to be changed to magic purchase UI.
                    magicPurchaseUI.SetActive(false);
                    playerController.money = playerController.money - 100;
                    playerIncomeUpdated = true;
                    Destroy(gameObject);
                });
                purchaseNoButton.onClick.AddListener(() => {
                    // Needs to be changed to magic purchase UI.
                    magicPurchaseUI.SetActive(false);
                });
            }
        }
    }
}