using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float x, y;

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        x = MobileInputProvider.Instance != null ? MobileInputProvider.Instance.Horizontal : 0f;
        y = MobileInputProvider.Instance != null ? MobileInputProvider.Instance.Vertical : 0f;
#else
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
#endif

        _moveInput = new Vector2(x, y).normalized;

        if (_animator != null)
        {
            _animator.SetFloat("Speed", _moveInput.magnitude);
        }

        if (Mathf.Abs(_moveInput.x) > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(_moveInput.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * moveSpeed;
    }
}