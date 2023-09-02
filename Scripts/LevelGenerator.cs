using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif

public class LevelGenerator : MonoBehaviour
{
    [Header("Components")]
    [Space]
    [SerializeField] private Transform player;

    [Header("Level Part Info")]
    [SerializeField] private Transform[] levelPart;
    [SerializeField] private Vector3 nextPartPosition;
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDelete;

    void Update()
    {
        DeletePlatform();
        GeneratePlatform();
    }

    private void GeneratePlatform()
    {
        while (Vector2.Distance(player.transform.position, nextPartPosition) < distanceToSpawn)
        {
            Transform part = levelPart[Random.Range(0, levelPart.Length)];
            Vector2 newPosition = new Vector2(nextPartPosition.x - part.Find("StartPoint").position.x, 0);
            Transform newPart = Instantiate(part, newPosition, transform.rotation, transform);
            nextPartPosition = newPart.Find("EndPoint").position;
        }
    }

    private void DeletePlatform()
    {
        if (transform.childCount > 0)
        {
            Transform partToDelete = transform.GetChild(0);

            if (Vector2.Distance(player.transform.position, partToDelete.transform.position) > distanceToDelete)
                Destroy(partToDelete.gameObject);
        }
    }
}
