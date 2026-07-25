using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inventoryText;

    private void OnEnable()
    {
        PlayerInventory.OnItemAdded += OnItemAdded;
    }

    private void OnDisable()
    {
        PlayerInventory.OnItemAdded -= OnItemAdded;
    }

    private void OnItemAdded(string itemId)
    {
        inventoryText.text += itemId + "\n";
    }
}