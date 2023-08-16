using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    private int amountOfCoins;
    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;

    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private SpriteRenderer[] coinImage;

    void Start()
    {
        for (int i = 0; i < coinImage.Length; i++)
        {
            coinImage[i].sprite = null;
        }

        amountOfCoins = Random.Range(minCoins, maxCoins);

        int coinOffset = amountOfCoins / 2;
        
        for (int i = 0; i < amountOfCoins; i++)
        {
            Vector3 offset = new Vector2(i - coinOffset, 0);

            Instantiate(coinPrefab, transform.position + offset, Quaternion.identity, transform);
        }
    }
}
