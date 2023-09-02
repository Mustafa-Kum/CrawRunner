using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    private bool canDetect;
    private BoxCollider2D boxCD;

    [Header("Components")]
    [Space]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Player player;
    [SerializeField] private float radius;

    private void Awake()
    {
        boxCD = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (canDetect)
            DetectLedge();
    }

    private void DetectLedge()
    {
        player.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, whatIsGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            canDetect = false;
    }

    private bool IsAnyPlatformNearby()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCD.bounds.center, boxCD.size, 0);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.GetComponent<PlatformController>() != null)
                return true;
        }
        return false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && !IsAnyPlatformNearby())
            canDetect = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
