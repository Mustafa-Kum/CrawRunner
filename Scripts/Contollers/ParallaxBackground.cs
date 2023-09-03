using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    private float length;
    private float xPosition;

    [Header("Parallax Settings")]
    [Space]
    [SerializeField] private float parallaxEffect;

    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        length = spriteRenderer.bounds.size.x;
        xPosition = transform.position.x;
    }

    void Update()
    {
        UpdateBackgroundPosition();
    }

    private void UpdateBackgroundPosition()
    {
        float distanceMoved = mainCamera.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = mainCamera.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
    }
}
