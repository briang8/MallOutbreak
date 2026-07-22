using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private bool requiresKey = false;
    [SerializeField] private string requiredKeyId = "";
    private bool _isOpen = false;

    public void Interact()
    {
        if (_isOpen) return;

        if (requiresKey && !InventoryHasKey())
        {
            Debug.Log("Door is locked, requires: " + requiredKeyId);
            return;
        }

        Open();
    }

    private bool InventoryHasKey()
    {
        
        PlayerInventory inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        return inv != null && inv.HasItem(requiredKeyId);
    }

    private void Open()
    {
        _isOpen = true;
        Debug.Log(gameObject.name + " opened");
        // Placeholder visual feedback for testing; will replace with animation later
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Color change to indicate door is open; will replace with animation later
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.green;
    }
}