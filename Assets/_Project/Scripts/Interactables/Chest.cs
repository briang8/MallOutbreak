using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemId = "Supplies";
    
    private bool _isOpened = false;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (_isOpened) return;
        _isOpened = true;

        Debug.Log("Chest opened, received: " + itemId);
        
        PlayerInventory inv = Object.FindAnyObjectByType<PlayerInventory>();
        inv?.AddItem(itemId);

        // Fire the Animator trigger set up in Chest Controller
        if (_animator != null)
        {
            _animator.SetTrigger("OpenTrigger");
        }
    }
}