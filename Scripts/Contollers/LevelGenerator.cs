using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform player;

    [Header("Level Part Info")]
    [SerializeField] private List<GameObject> levelPartPrefabs; // Kar���k s�rayla seviye par�alar�n� i�eren liste
    [SerializeField] private Vector3 nextPartPosition;
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDelete;
    [SerializeField] private int minPrefabIndex = 5; // Minimum prefab index
    [SerializeField] private int maxPrefabIndex = 10; // Maximum prefab index

    private int generateCounter;
    private Transform partToDelete;
    private Vector2 newPosition;

    private List<GameObject> levelPartPool; // Seviye par�alar�n� nesne havuzunda saklamak i�in
    private Dictionary<GameObject, GameObject> spawnedParts; // Seviye par�alar�n� ve olu�turulan nesneleri izlemek i�in

    private void Awake()
    {
        InitializeLevelPartPool();
    }

    private void InitializeLevelPartPool()
    {
        levelPartPool = new List<GameObject>();
        spawnedParts = new Dictionary<GameObject, GameObject>();

        foreach (GameObject levelPartPrefab in levelPartPrefabs)
        {
            GameObject newLevelPart = Instantiate(levelPartPrefab, transform);
            newLevelPart.SetActive(false);
            levelPartPool.Add(newLevelPart);
            spawnedParts[levelPartPrefab] = newLevelPart;
        }
    }

    private void Update()
    {
        DeletePlatform();
        GeneratePlatform();
    }

    private void GeneratePlatform()
    {
        if (Vector2.Distance(player.transform.position, nextPartPosition) < distanceToSpawn)
        {
            int randomIndex = Random.Range(minPrefabIndex, maxPrefabIndex + 1); // Belirli aral�kta bir rastgele prefab
            GameObject levelPartPrefab = levelPartPrefabs[randomIndex];

            if (!spawnedParts[levelPartPrefab].activeSelf)
            {
                newPosition = new Vector2(nextPartPosition.x - levelPartPrefab.transform.Find("StartPoint").position.x, 0);
                spawnedParts[levelPartPrefab].transform.position = newPosition;
                spawnedParts[levelPartPrefab].SetActive(true);
                nextPartPosition = spawnedParts[levelPartPrefab].transform.Find("EndPoint").position;
                generateCounter++;
                return; // Bir kez spawn etti�imizde ��k
            }
        }
    }

    private void DeletePlatform()
    {
        foreach (GameObject partToDelete in levelPartPool)
        {
            if (partToDelete.activeSelf && Vector2.Distance(player.transform.position, partToDelete.transform.position) > distanceToDelete)
            {
                partToDelete.SetActive(false);
            }
        }
    }
}
