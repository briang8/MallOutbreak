using UnityEngine;

public class DashAbility : MonoBehaviour, IAbility
{
    [SerializeField] private float dashForce = 10f;
    public float Cooldown => 3f;

    public void Activate(GameObject user)
    {
        Debug.Log("Dashing!");
        Rigidbody2D rb = user.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 dashDirection = rb.linearVelocity.normalized;
            if (dashDirection == Vector2.zero) dashDirection = Vector2.up; // default direction if standing still
            rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        }
    }
}