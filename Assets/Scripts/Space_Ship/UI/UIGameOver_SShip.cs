using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIGameOver_SShip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    Scorekeeper_SShip scorekeeper;

    void Awake()
    {
        scorekeeper = FindObjectOfType<Scorekeeper_SShip>();
    }
    void Start()
    {
        scoreText.text = "Your scored:\n" + scorekeeper.getScore();
    }
}
