using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct ColorToSell
{
    public Color color;
    public int price;
}

public enum ColorType
{
    playerColor,
    platformColor
}

public class UI_Shop : MonoBehaviour
{
    [Header("Text")]
    [Space]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI notifyText;

    [Header("Platform Color")]
    [Space]
    [SerializeField] private GameObject platformColorButton;
    [SerializeField] private Transform platformColorParent;
    [SerializeField] private Image platformDisplay;
    [SerializeField] private ColorToSell[] platformColors;

    [Header("Player Color")]
    [Space]
    [SerializeField] private GameObject playerColorButton;
    [SerializeField] private Transform playerColorParent;
    [SerializeField] private Image playerDisplay;
    [SerializeField] private ColorToSell[] playerColors;

    void Start()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");

        for (int i = 0; i < platformColors.Length; i++)
        {
            Color color = platformColors[i].color;
            int price = platformColors[i].price;

            GameObject newButton = Instantiate(platformColorButton, platformColorParent);
            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");
            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.platformColor));
        }

        for (int i = 0; i < playerColors.Length; i++)
        {
            Color color = playerColors[i].color;
            int price = playerColors[i].price;

            GameObject newButton = Instantiate(playerColorButton, playerColorParent);
            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");
            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.playerColor));
        }
    }

    public void PurchaseColor(Color color, int price, ColorType colorType)
    {
        AudioManager.instance.PlaySFX(4);

        Color _platformColor = new Color(PlayerPrefs.GetFloat("PlatformColorR"),
                                         PlayerPrefs.GetFloat("PlatformColorG"),
                                         PlayerPrefs.GetFloat("PlatformColorB"),
                                         PlayerPrefs.GetFloat("PlatformColorA", 1));

        Color _playerColor = new Color(PlayerPrefs.GetFloat("ColorR"),
                                       PlayerPrefs.GetFloat("ColorG"),
                                       PlayerPrefs.GetFloat("ColorB"),
                                       PlayerPrefs.GetFloat("ColorA", 1));

        if (colorType == ColorType.platformColor && color != _platformColor && EnoughMoney(price))
        {
            GameManager.instance.platformColor = color;
            GameManager.instance.SavePlatformColor(color.r, color.g, color.b);
            platformDisplay.color = color;
            StartCoroutine(Notify("Purchase Successful"));
        }
        else if (colorType == ColorType.playerColor && color != _playerColor && EnoughMoney(price))
        {
            GameManager.instance.player.GetComponent<SpriteRenderer>().color = color;
            GameManager.instance.SaveColor(color.r, color.g, color.b);
            playerDisplay.color = color;
            StartCoroutine(Notify("Purchase Successful"));
        }
    }

    private bool EnoughMoney(int price)
    {
        int myCoins = PlayerPrefs.GetInt("Coins");

        if (myCoins > price)
        {
            int newAmountOfCoins = myCoins - price;
            PlayerPrefs.SetInt("Coins", newAmountOfCoins);
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");
            return true;
        }
        return false;
    }

    IEnumerator Notify(string text)
    {
        notifyText.text = text;
        yield return new WaitForSeconds(1);
        notifyText.text = "Shop";
    }
}
