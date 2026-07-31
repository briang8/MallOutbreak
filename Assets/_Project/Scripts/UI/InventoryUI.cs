using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private GameObject inventoryPanel;

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
        if (inventoryPanel != null) inventoryPanel.SetActive(true);
        inventoryText.text += itemId + "\n";
    }
}