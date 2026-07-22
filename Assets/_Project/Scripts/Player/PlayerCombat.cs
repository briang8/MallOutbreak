using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private LayerMask enemyLayer;

    public void Attack()
    {
        // finds anything on the enemy layer within range and damages it
        Debug.Log("Attacking!");
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (var hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // visual debug circle in Scene view, doesn't affect gameplay
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Update()
    {
    if (Input.GetKeyDown(KeyCode.Space)) Attack();
    }

}


