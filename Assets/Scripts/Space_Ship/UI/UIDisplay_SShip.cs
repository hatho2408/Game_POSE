using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay_SShip : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health_SShip playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    Scorekeeper_SShip scorekeeper;

    void Awake()
    {
        scorekeeper = FindObjectOfType<Scorekeeper_SShip>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = playerHealth.getHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth.getHealth();
        scoreText.text = scorekeeper.getScore().ToString("000000000");
    }
}
