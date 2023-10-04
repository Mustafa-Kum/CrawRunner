using TMPro;
using UnityEngine;

public class UI_EndGame : MonoBehaviour
{
    [Header("Text Info")]
    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI coins;

    void Start()
    {
        GameManager manager = GameManager.instance;

        if (manager.distance > 0)
            distance.text = ": " + manager.distance.ToString("#,#") + " M";

        if (manager.coins > 0)
            coins.text = ": " + manager.coins.ToString("#,#");
    }
}
