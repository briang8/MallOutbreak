using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemId = "Supplies";
    private bool _isOpened = false;

    public void Interact()
    {
        if (_isOpened) return;
        _isOpened = true;

        Debug.Log("Chest opened, received: " + itemId);
        PlayerInventory inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        inv?.AddItem(itemId);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.gray; // visibly "emptied"
    }
}