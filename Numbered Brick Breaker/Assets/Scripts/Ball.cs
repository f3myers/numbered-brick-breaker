
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float launchSpeed = 7f;

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

        Vector2 velocity = rb.linearVelocity;

        if (velocity.sqrMagnitude > 0f)
        {
            rb.linearVelocity = velocity.normalized * launchSpeed;
        }
    }
}
