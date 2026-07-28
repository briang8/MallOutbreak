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
        float x, y;
        #if UNITY_ANDROID || UNITY_IOS
            x = MobileInputProvider.Instance != null ? MobileInputProvider.Instance.Horizontal : 0f;
            y = MobileInputProvider.Instance != null ? MobileInputProvider.Instance.Vertical : 0f;
        #else
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        #endif

    _moveInput = new Vector2(x, y).normalized;
    
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * moveSpeed;
    }
}