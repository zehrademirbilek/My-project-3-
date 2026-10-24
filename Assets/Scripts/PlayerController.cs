using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private float moveSpeed ;
    [SerializeField] private float jumpForce;
    private Vector2 moveInput;
  public void OnMove(InputValue value)
    {
         moveInput = value.Get<Vector2>();
        
    }

    public void OnJump(InputValue value)
    {
        playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x,  jumpForce);
    }
    private void FixedUpdate()
    {
        playerRigidbody.linearVelocity = new Vector2(moveInput.x*moveSpeed , playerRigidbody.linearVelocity.y);
    }
}
