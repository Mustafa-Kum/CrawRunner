using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [Header("Components")]
    [Space]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private SpriteRenderer[] coinImage;
    
    [Header("Coin Info")]
    [Space]
    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;

    void Start()
    {
        CleanCoinImages();
        GenerateCoins();
    }

    private void CleanCoinImages()
    {
        for (int i = 0; i < coinImage.Length; i++)
        {
            coinImage[i].sprite = null;
        }
    }

    private void GenerateCoins()
    {
        int amountOfCoins = Random.Range(minCoins, maxCoins);
        int coinOffset = amountOfCoins / 2;

        for (int i = 0; i < amountOfCoins; i++)
        {
            Vector3 offset = new Vector2(i - coinOffset, 0);
            Instantiate(coinPrefab, transform.position + offset, Quaternion.identity, transform);
        }
    }

}
