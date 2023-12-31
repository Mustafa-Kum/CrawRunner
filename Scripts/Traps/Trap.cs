using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Spawn Info")]
    [Space]
    [SerializeField] protected float chanceToSpawn = 50;

    protected virtual void Start()
    {
        bool canSpawn = chanceToSpawn >= Random.Range(0, 100);

        if (!canSpawn)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            collision.GetComponent<Player>().Damage();
    }
}
