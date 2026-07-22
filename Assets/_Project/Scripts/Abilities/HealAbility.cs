using UnityEngine;

public class HealAbility : MonoBehaviour, IAbility
{
    [SerializeField] private int healAmount = 20;
    public float Cooldown => 8f;

    public void Activate(GameObject user)
    {
        Debug.Log("Healing!");
        PlayerHealth health = user.GetComponent<PlayerHealth>();
        health?.Heal(healAmount);
    }
}