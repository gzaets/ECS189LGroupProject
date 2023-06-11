using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsController : MonoBehaviour
{

    [SerializeField]
    public Slider healthSlider;
    [SerializeField]
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = playerController.health;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerController.health;
    }
}
