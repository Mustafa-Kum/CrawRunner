using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_EndGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI score;

    void Start()
    {
        GameManager manager = GameManager.instance;

        //Time.timeScale = 0;

        if (manager.distance > 0)
            distance.text = "Distance: " + manager.distance.ToString("#,#") + " M";

        if (manager.coins > 0)
            coins.text = "Coins: " + manager.coins.ToString("#,#");

        if (manager.score > 0)
            score.text = "Score: " + manager.score.ToString("#,#");
    }
}
