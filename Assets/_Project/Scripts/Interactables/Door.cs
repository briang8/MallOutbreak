using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private bool requiresKey = false;
    [SerializeField] private string requiredKeyId = "";
    [SerializeField] private bool isLevelExit = false; 
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
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.green;

        AudioManager.Instance.PlayDoorOpen();

        if (isLevelExit)
        {
            LevelManager.Instance.CompleteCurrentLevel();
        }
    }

    public void SetLocked(bool locked)
    {
        requiresKey = locked;
    }
}