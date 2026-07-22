using UnityEngine;

public class Collectible : MonoBehaviour, ICollectable
{
    [SerializeField] private string itemId = "Ammo";

    // collectibles auto-pick-up on touch rather than needing E, using a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    public void Collect(GameObject collector)
    {
        Debug.Log(itemId + " collected");
        PlayerInventory inv = collector.GetComponent<PlayerInventory>();
        inv?.AddItem(itemId);

        gameObject.SetActive(false);
    }
}