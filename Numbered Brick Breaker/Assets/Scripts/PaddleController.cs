
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 8f;

    [Header("Límites de pantalla")]
    [SerializeField] private float minX = -7f;
    [SerializeField] private float maxX = 7f;

    private void Update()
    {
        // Leer el movimiento horizontal del jugador.
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        // Mover la pala.
        Vector3 position = transform.position;
        position.x += horizontalInput * moveSpeed * Time.deltaTime;

        // Mantener la pala dentro de los límites.
        position.x = Mathf.Clamp(position.x, minX, maxX);

        transform.position = position;
    }
}