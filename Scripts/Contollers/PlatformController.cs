using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Components")]
    [Space]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private SpriteRenderer headerSr;

    private void Start()
    {
        headerSr.transform.parent = transform.parent;
        headerSr.transform.localScale = new Vector2(sr.bounds.size.x, .2f);
        headerSr.transform.position = new Vector2(transform.position.x, sr.bounds.max.y - .1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            headerSr.color = GameManager.instance.platformColor;
    }
}
