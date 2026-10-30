
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float launchSpeed = 7f;

    [Header("Rebote en la pala")]
    [SerializeField] private float bounceAngle = 60f;

    private Rigidbody2D rb;
    private bool hasLaunched = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.linearVelocity = Vector2.zero;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasLaunched)
            return;

        // Comprobar si la pelota ha golpeado la pala.
        PaddleController paddle = collision.collider.GetComponent<PaddleController>();

        if (paddle != null)
        {
            // Calcular dónde ha golpeado la pelota.
            float paddleWidth = collision.collider.bounds.extents.x;
            float hitPosition = (transform.position.x - collision.transform.position.x) / paddleWidth;

            hitPosition = Mathf.Clamp(hitPosition, -1f, 1f);

            // Convertir el punto de impacto en un ángulo.
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
            // Mantener la velocidad al rebotar contra paredes y bloques.
            if (rb.linearVelocity.sqrMagnitude > 0f)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * launchSpeed;
            }
        }
    }
}
