using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // basic input for now, New Input System swap comes in Phase 12 (platform input)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _moveInput = new Vector2(x, y).normalized;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * moveSpeed;
    }
}