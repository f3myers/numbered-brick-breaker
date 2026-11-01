
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float launchSpeed = 7f;

    [Header("Rebote en la pala")]
    [SerializeField] private float bounceAngle = 60f;

    private Rigidbody2D rb;
    private bool hasLaunched = false;

    private Vector3 startingPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        ResetBall();
    }

    private void Update()
    {
        if (!hasLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }

    private void Launch()
    {
        hasLaunched = true;

        Vector2 launchDirection = new Vector2(0.5f, 1f).normalized;
        rb.linearVelocity = launchDirection * launchSpeed;
    }

    public void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = startingPosition;
        hasLaunched = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasLaunched)
            return;

        PaddleController paddle = collision.collider.GetComponent<PaddleController>();

        if (paddle != null)
        {
            float paddleWidth = collision.collider.bounds.extents.x;
            float hitPosition = (transform.position.x - collision.transform.position.x) / paddleWidth;

            hitPosition = Mathf.Clamp(hitPosition, -1f, 1f);

            float angle = hitPosition * bounceAngle;
            float angleInRadians = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(
                Mathf.Sin(angleInRadians),
                Mathf.Cos(angleInRadians)
            );

            rb.linearVelocity = direction * launchSpeed;
        }
        else
        {
            if (rb.linearVelocity.sqrMagnitude > 0f)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * launchSpeed;
            }
        }
    }
}
