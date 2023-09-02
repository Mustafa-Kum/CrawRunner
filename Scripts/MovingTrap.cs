using UnityEngine;

public class MovingTrap : Trap
{
    private int movePointIndex;
    
    [Header("Trap Info")]
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    protected override void Start()
    {
        base.Start();
        InitializeTrapPosition();
    }

    private void Update()
    {
        MoveTowardsNextPoint();
        RotateTrap();
    }
    
    private void InitializeTrapPosition()
    {
        transform.position = movePoint[0].position;
    }

    private void MoveTowardsNextPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoint[movePointIndex].position) < .25f)
        {
            movePointIndex++;

            if (movePointIndex >= movePoint.Length)
                movePointIndex = 0;
        }
    }

    private void RotateTrap()
    {
        if (transform.position.x > movePoint[movePointIndex].position.x)
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        else
            transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
