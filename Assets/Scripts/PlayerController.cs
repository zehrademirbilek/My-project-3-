using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    // --- DOUBLE JUMP İÇİN EKLENEN DEĞİŞKENLER ---
    [SerializeField] private int maxJumps = 2;
    private int jumpsRemaining;

    private Vector2 moveInput;

    private void Start()
    {
        jumpsRemaining = maxJumps;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        // Yalnızca tuşa basıldığında ve zıplama hakkı kaldığında çalışır
        if (value.isPressed && jumpsRemaining > 0)
        {
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, jumpForce);
            jumpsRemaining--; // Hakkı 1 azaltır
        }
    }

    private void FixedUpdate()
    {
        playerRigidbody.linearVelocity = new Vector2(moveInput.x * moveSpeed, playerRigidbody.linearVelocity.y);
    }

    // --- YERE DEĞDİĞİNDE HAKLARI YENİLEME ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpsRemaining = maxJumps;
    }
}